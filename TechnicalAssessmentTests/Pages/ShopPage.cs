using Microsoft.Playwright;
using TechnicalAssessmentTests.Components;
using TechnicalAssessmentTests.Components.Collection;
using TechnicalAssessmentTests.Entity;
using static Microsoft.Playwright.Assertions;

namespace TechnicalAssessmentTests.Pages;

public sealed class ShopPage(IPage page) : AbstractPage(page)
{
    public MenuComponent MenuComponent { get; } = new(page, ".navbar-inner");
    public StockItemsCollection StockItemsCollection { get; } = new(page);

    protected override string Fragment => "/shop";

    public async Task NavigateToCartPage()
    {
        await MenuComponent.TopRightMenu.CartLink.ClickAsync();
    }
}

public class ShopPageAssertScenarioManager(ShopPage page)
{
    private readonly ShopPageAssertManager _assetManager = new(page);

    public async Task GoToShopPageAndAddToCartAsync(
        Dictionary<string, Item> purchaseList
    )
    {
        await page.GoToAsync();
        await _assetManager.AssertMenuComponentVisibleAsync();
        await _assetManager.AssertItemsAndDataInsideVisibleAsync();

        await page.StockItemsCollection.AddToCartAsync(purchaseList);
        await page.NavigateToCartPage();
    }
}

public sealed class ShopPageAssertManager(ShopPage page)
{
    public async Task AssertMenuComponentVisibleAsync()
    {
        await page.MenuComponent.AssertNodeVisibleAsync();
        await page.MenuComponent.AssertLeftMenuVisibleAsync();
        await page.MenuComponent.AssertRightMenuVisibleAsync();
    }

    public async Task AssertItemsAndDataInsideVisibleAsync()
    {
        await Expect(page.StockItemsCollection.Collection)
            .ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = 2000 });
        var count = await page.StockItemsCollection.Node.CountAsync();

        for (var i = 0; i < count; i++)
        {
            var item = page.StockItemsCollection.Node.Nth(i);

            await Expect(item.Locator(StockItemsCollection.SingleItemTitleSelector)).ToBeVisibleAsync();
            await Expect(item.Locator(StockItemsCollection.SingleItemPriceSelector)).ToBeVisibleAsync();
            await Expect(item.Locator(StockItemsCollection.SingleSuccessButtonSelector)).ToBeVisibleAsync();
        }
    }
}
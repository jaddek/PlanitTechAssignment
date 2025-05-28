using Microsoft.Playwright;
using TechnicalAssessmentTests.Entity;

namespace TechnicalAssessmentTests.Components.Collection;

public class StockItemsCollection(IPage page) : AbstractComponent
{
    private const string ItemsSelector = ".products";
    private const string SingleItemSelector = ".product";
    private const string SingleItemTitleSelector = ".product-title";
    private const string SingleItemPriceSelector = ".product-price";
    private const string SingleSuccessButtonSelector = "a.btn-success";
    private const string CurrencySign = "$";

    public override ILocator Node { get; } = page.Locator(ItemsSelector);

    public async Task AddToCartAsync(Dictionary<string, Item> shoppingList)
    {
        var itemsOnShelf = Node.Locator(SingleItemSelector);
        var count = await itemsOnShelf.CountAsync();

        for (var i = 0; i < count; i++)
        {
            var itemOnShelf = itemsOnShelf.Nth(i);

            var title = await itemOnShelf.Locator(SingleItemTitleSelector).InnerTextAsync();

            if (shoppingList.TryGetValue(title.Trim(), out var shoppingListItem))
                await ProcessItemAsync(itemOnShelf, shoppingListItem);
        }
    }

    private async Task ProcessItemAsync(ILocator itemOnShelf, Item shoppingListItem)
    {
        var priceText = await itemOnShelf.Locator(SingleItemPriceSelector).TextContentAsync();
        if (!string.IsNullOrWhiteSpace(priceText))
        {
            var priceClean = priceText.Replace(CurrencySign, "").Trim();
            if (decimal.TryParse(priceClean, out var price))
            {
                shoppingListItem.Price = price;
            }
        }

        await itemOnShelf.Locator(SingleSuccessButtonSelector).ClickAsync(new LocatorClickOptions
        {
            ClickCount = shoppingListItem.Qty
        });
    }
}
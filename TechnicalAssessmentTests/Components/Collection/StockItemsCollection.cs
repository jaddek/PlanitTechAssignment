using Microsoft.Playwright;
using TechnicalAssessmentTests.Entity;

namespace TechnicalAssessmentTests.Components.Collection;

public class StockItemsCollection(IPage page) : AbstractComponent
{
    public const string CollectionSelector = ".products";
    public const string SingleItemSelector = ".product";
    public const string SingleItemTitleSelector = ".product-title";
    public const string SingleItemPriceSelector = ".product-price";
    public const string SingleSuccessButtonSelector = "a.btn-success";
    private const string CurrencySign = "$";

    public readonly ILocator Collection = page.Locator(CollectionSelector);
    public override ILocator Node { get; } = page.Locator(SingleItemSelector);
    
    public async Task AddToCartAsync(Dictionary<string, Item> shoppingList)
    {
        var count = await Node.CountAsync();

        for (var i = 0; i < count; i++)
        {
            var itemOnShelf = Node.Nth(i);

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
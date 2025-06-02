using Microsoft.Playwright;

namespace TechnicalAssessmentTests.Components.Collection;

public sealed class CartItemsCollection(IPage page) : AbstractComponent
{
    private const string CollectionSelector = ".cart-items";
    private const string SingleItemSelector = ".cart-item";
    private const string TotalSelector = ".total";
    public readonly ILocator Collection = page.Locator(CollectionSelector);
    public readonly ILocator Total = page.Locator(TotalSelector);

    public override ILocator Node { get; } = page.Locator(SingleItemSelector);
}
using Microsoft.Playwright;

namespace TechnicalAssessmentTests.Components.Collection;

public sealed class CartItemsCollection(IPage page) : AbstractComponent
{
    private const string ItemSelector = ".cart-items";
    private const string SingleItemSelector = ".cart-item";
    public override ILocator Node { get; } = page.Locator(ItemSelector);
    
    public ILocator GetItems()
    {
        return Node.Locator(SingleItemSelector);
    }
}
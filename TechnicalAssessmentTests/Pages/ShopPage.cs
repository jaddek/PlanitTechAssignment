using Microsoft.Playwright;
using TechnicalAssessmentTests.Components;
using TechnicalAssessmentTests.Components.Collection;

namespace TechnicalAssessmentTests.Pages;

public sealed class ShopPage(IPage page) : AbstractPage(page)
{
    public MenuComponent MenuComponent { get; } = new(page, ".nav-collapse");
    public StockItemsCollection StockItemsCollection { get; } = new(page);

    protected override string Fragment => "/shop";
}
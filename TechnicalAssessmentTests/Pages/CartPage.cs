using Microsoft.Playwright;
using TechnicalAssessmentTests.Components.Collection;

namespace TechnicalAssessmentTests.Pages;

public sealed class CartPage(IPage page) : AbstractPage(page)
{
    protected override string Fragment => "/#/cart";
    
    public CartItemsCollection CartItemsCollection { get; } = new(page);
}
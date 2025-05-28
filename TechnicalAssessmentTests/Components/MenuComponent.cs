using Microsoft.Playwright;
using TechnicalAssessmentTests.Components.Menu;

namespace TechnicalAssessmentTests.Components;

public class MenuComponent(IPage page, string selector) : AbstractComponent
{
    public override ILocator Node { get; } = page.Locator(selector);
    
    public TopLeftMenu TopLeftMenu { get; } = new(page);
    public TopRightMenu TopRightMenu { get; } = new(page);
}
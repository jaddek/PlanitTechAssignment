using Microsoft.Playwright;

namespace TechnicalAssessmentTests.Components.Menu;

public class TopRightMenu(IPage page) : AbstractComponent
{
    public override ILocator Node { get; } = page.Locator("ul").Last;

    public CartTab CartTab { get; } = new(page);
}
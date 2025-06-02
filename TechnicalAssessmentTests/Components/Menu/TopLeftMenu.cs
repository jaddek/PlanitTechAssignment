using Microsoft.Playwright;

namespace TechnicalAssessmentTests.Components.Menu;

public class TopLeftMenu(IPage page) : AbstractComponent
{
    public override ILocator Node { get; } = page.Locator("ul").First;

    public ContactTab ContactLink { get; } = new(page);
}
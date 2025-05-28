using Microsoft.Playwright;

namespace TechnicalAssessmentTests.Components;

public class FormComponent(IPage page, string selector) : AbstractComponent
{
    public override ILocator Node { get; } = page.Locator(selector);
}
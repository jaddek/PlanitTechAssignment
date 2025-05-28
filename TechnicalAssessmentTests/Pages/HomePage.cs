using Microsoft.Playwright;
using TechnicalAssessmentTests.Components;

namespace TechnicalAssessmentTests.Pages;

public sealed class HomePage : AbstractPage
{
    public MenuComponent MenuComponent { get; }

    protected override string Fragment => "/";

    public HomePage(IPage page) : base(page)
    {
        MenuComponent = new MenuComponent(Page, ".nav-collapse");
    }
}
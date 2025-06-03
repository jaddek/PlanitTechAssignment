using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;
using TechnicalAssessmentTests.Components.Menu;


namespace TechnicalAssessmentTests.Components;

public class MenuComponent(IPage page, string selector) : AbstractComponent
{
    public override ILocator Node { get; } = page.Locator(selector);
    
    public TopLeftMenu TopLeftMenu { get; } = new(page);
    public TopRightMenu TopRightMenu { get; } = new(page);
    
    public async Task AssertNodeVisibleAsync()
    {
        await Expect(Node).ToBeVisibleAsync();
    }
    public async Task AssertLeftMenuVisibleAsync()
    {
        await Expect(TopLeftMenu.ContactLink.Node).ToBeVisibleAsync();
    }
    
    public async Task AssertRightMenuVisibleAsync()
    {
        await Expect(TopRightMenu.CartLink.Node).ToBeVisibleAsync();
    }
}
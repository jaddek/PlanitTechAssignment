using Microsoft.Playwright;
using TechnicalAssessmentTests.Components;
using static Microsoft.Playwright.Assertions;


namespace TechnicalAssessmentTests.Pages;

public sealed class HomePage : AbstractPage
{
    public MenuComponent MenuComponent { get; }

    protected override string Fragment => "/";

    public HomePage(IPage page) : base(page)
    {
        MenuComponent = new MenuComponent(Page, ".navbar-inner");
    }

    public async Task NavigateToContactPage()
    {
        await MenuComponent.TopLeftMenu.ContactLink.ClickAsync();
    }
}

public class HomePageAssertScenarioManager(HomePage page)
{
    private readonly HomePageAssertManager _assetManager = new(page);

    public async Task GoToAssertContactLinkAvailableAndNavigateToContactPageAsync(
    )
    {
        await page.GoToAsync();
        await AssertContactLinkAvailableAndNavigateToContactPageAsync();
    }
    
    public async Task AssertContactLinkAvailableAndNavigateToContactPageAsync(
    )
    {
        await _assetManager.AssertMenuComponentVisibleAsync();
        await page.NavigateToContactPage();
    }
}

public sealed class HomePageAssertManager(HomePage page)
{
    public async Task AssertMenuComponentVisibleAsync()
    {
        await page.MenuComponent.AssertNodeVisibleAsync();
        await page.MenuComponent.AssertLeftMenuVisibleAsync();
        await page.MenuComponent.AssertRightMenuVisibleAsync();
    }
}
using System.Text.RegularExpressions;
using Microsoft.Playwright;

namespace TechnicalAssessmentTests.Components.Menu;

public class CartTab(IPage page) : AbstractComponent
{
    private const string CartUrl = "**/#/cart";
    private const string CartRegex = @"Cart.*";

    public override ILocator Node =>
        page.GetByRole(AriaRole.Link, new PageGetByRoleOptions
        {
            NameRegex = new Regex(CartRegex, RegexOptions.IgnoreCase)
        });

    public async Task ClickAsync()
    {
        await Node.ClickAsync();
        await page.WaitForURLAsync(CartUrl, new PageWaitForURLOptions { Timeout = 5000 });
    }
}
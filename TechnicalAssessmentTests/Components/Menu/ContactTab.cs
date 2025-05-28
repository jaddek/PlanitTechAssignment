using Microsoft.Playwright;

namespace TechnicalAssessmentTests.Components.Menu;

public class ContactTab(IPage page) : AbstractComponent
{
    private const string ContactLinkName = "Contact";
    private const string ContactUrl = "**/#/contact";

    public override ILocator Node { get; } = page.GetByRole(AriaRole.Link, new PageGetByRoleOptions
    {
        Name = ContactLinkName
    });

    public async Task ClickAsync()
    {
        await Node.First.ClickAsync();
        await page.WaitForURLAsync(ContactUrl, new PageWaitForURLOptions { Timeout = 5000 });
    }
}
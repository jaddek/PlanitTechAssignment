using Microsoft.Playwright;

namespace TechnicalAssessmentTests.Components;

public abstract class AbstractComponent
{
    public abstract ILocator Node { get; }

    public async Task<bool> IsVisibleAsync()
    {
        return await Node.IsVisibleAsync();
    }
}
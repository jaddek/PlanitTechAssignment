using System.Text.RegularExpressions;
using Microsoft.Playwright;

namespace TechnicalAssessmentTests.Pages;

public abstract class AbstractPage(IPage page)
{
    protected readonly IPage Page = page;

    protected virtual string Fragment => "";
    protected virtual string Path => "/";

    private string GetPageUrl()
    {
        return new UriBuilder
        {
            Scheme = AppConfig.Scheme,
            Host = AppConfig.Host,
            Fragment = Fragment,
            Path = Path
        }.ToString();
    }

    public async Task GoToAsync()
    {
        await Page.GotoAsync(GetPageUrl());
    }
}
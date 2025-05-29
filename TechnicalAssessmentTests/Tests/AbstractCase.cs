using Microsoft.Playwright.Xunit;
using Xunit.Abstractions;

namespace TechnicalAssessmentTests.Tests;

public class AbstractCase(ITestOutputHelper output) : PageTest
{
    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        await Context.Tracing.StartAsync(new()
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });
    }

    public override async Task DisposeAsync()
    {
        var traceFileName = $"trace-{Guid.NewGuid()}.zip";
        var traceFilePath = Path.Combine(AppConfig.PlaywrightTraceDir, traceFileName);

        await Context.Tracing.StopAsync(new() { Path = traceFilePath });

        output.WriteLine($"Trace saved to: {traceFilePath}");
        await base.DisposeAsync();
    }
}
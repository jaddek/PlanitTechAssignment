namespace TechnicalAssessmentTests;

public static class AppConfig
{
    public static string Scheme => Environment.GetEnvironmentVariable("SCHEME") ?? "https";
    public static string Host => Environment.GetEnvironmentVariable("HOST") ?? "jupiter.cloud.planittesting.com";
    public static string PlaywrightTraceDir => Environment.GetEnvironmentVariable("PLAYWRIGHT_TRACE_DIR") ?? "./traces";
}
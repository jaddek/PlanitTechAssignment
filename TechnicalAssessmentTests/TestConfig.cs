namespace TechnicalAssessmentTests;

public static class TestConfig
{
    public static string Scheme => Environment.GetEnvironmentVariable("SCHEME") ?? "https";
    public static string Host => Environment.GetEnvironmentVariable("HOST") ?? "jupiter.cloud.planittesting.com";
}
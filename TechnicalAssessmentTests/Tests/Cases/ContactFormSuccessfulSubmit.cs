using TechnicalAssessmentTests.Pages;
using Xunit.Abstractions;

namespace TechnicalAssessmentTests.Tests.Cases;

// Test case 2:
// 1. From the home page go to contact page
// 2. Populate mandatory fields
// 3. Click submit button
// 4. Validate successful submission message
// Note: Run this test 5 times to ensure 100% pass rate
[Collection("Contact Form Submission Tests")]
[Trait("Group", "ContactFormSubmission")]
public class ContactFormSuccessfulSubmit(ITestOutputHelper output) : AbstractCase(output)
{
    [Theory(DisplayName = "Submit contact form successfully with valid input")]
    [InlineData("forename 1", "email+1@test.com", "Message 1")]
    [InlineData("forename 20", "email+20@test.com", "Message 20")]
    [InlineData("forename 3", "email+3@test.com", "Message 3")]
    [InlineData("forename 4", "email+4@test.com", "Message 4")]
    [InlineData("forename 50", "email+50@test.com", "Message 50")]
    public async Task GivenEmptyContactForm_WhenSubmitted_SuccessfulSend(
        string forename,
        string email,
        string message
    )
    {
        var homePageScenarioManager = new HomePageAssertScenarioManager(new HomePage(Page));
        await homePageScenarioManager.GoToAssertContactLinkAvailableAndNavigateToContactPageAsync();

        var contactPageScenarioManager = new ContactPageAssertScenarioManager(new ContactPage(Page));
        await contactPageScenarioManager.ValidateFormSuccessSubmitAsync(forename, email, message);
    }
}
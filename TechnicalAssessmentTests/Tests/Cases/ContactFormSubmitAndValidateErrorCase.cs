using TechnicalAssessmentTests.Pages;
using Xunit.Abstractions;

namespace TechnicalAssessmentTests.Tests.Cases;

// Test case 1:
// 1. From the home page go to contact page
// 2. Click submit button
// 3. Verify error messages
// 4. Populate mandatory fields
// 5. Validate errors are gone
[Collection("Contact Form Validation Tests")]
[Trait("Group", "ContactFormValidation")]
public class ContactFormSubmitAndValidateErrorCase(ITestOutputHelper output) : AbstractCase(output)
{
    [Theory]
    [InlineData("Forename", "email@email.com", "message")]
    public async Task ContactForm_ShouldShowAndClearValidationErrorsWithValidData(
        string forename,
        string email,
        string message
    )
    {
        var homePageScenarioManager = new HomePageAssertScenarioManager(new HomePage(Page));
        await homePageScenarioManager.GoToAssertContactLinkAvailableAndNavigateToContactPageAsync();

        var contactPageScenarioManager = new ContactPageAssertScenarioManager(new ContactPage(Page));
        await contactPageScenarioManager.ValidateFormErrorMessagesGoneAfterValidPopulation(forename, email, message);
    }
}
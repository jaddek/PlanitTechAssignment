using TechnicalAssessmentTests.Pages;

namespace TechnicalAssessmentTests.Tests.Cases;

// Test case 1:
// 1. From the home page go to contact page
// 2. Click submit button
// 3. Verify error messages
// 4. Populate mandatory fields
// 5. Validate errors are gone
public class ContactFormSubmitAndValidateErrorCase : AbstractCase
{
    [Theory]
    [InlineData("Forename", "email@email.com", "message")]
    public async Task ContactForm_ShouldShowAndClearValidationErrorsWithValidData(
        string forename,
        string email,
        string message
    )
    {
        var homePage = new HomePage(Page);
        await homePage.GoToAsync();

        Assert.True(await homePage.MenuComponent.TopLeftMenu.Contact.IsVisibleAsync(),
            "Contact link should be visible in top left menu");

        await homePage.MenuComponent.TopLeftMenu.Contact.ClickAsync();

        var contactPage = new ContactPage(Page);
        var form = contactPage.FormComponent;

        await Expect(form.Forename()).ToBeVisibleAsync();
        await Expect(form.Email()).ToBeVisibleAsync();
        await Expect(form.Message()).ToBeVisibleAsync();
        await Expect(form.Submit()).ToBeVisibleAsync();

        await form.Submit().ClickAsync();
        
        await Expect(form.ForenameRequiredError()).ToBeVisibleAsync();
        await Expect(form.EmailRequiredError()).ToBeVisibleAsync();
        await Expect(form.MessageRequiredError()).ToBeVisibleAsync();

        await form.Forename().FillAsync(forename);
        await form.Email().FillAsync(email);
        await form.Message().FillAsync(message);
        
        await form.Submit().ClickAsync();

        await Expect(form.ForenameRequiredError()).ToBeHiddenAsync();
        await Expect(form.EmailRequiredError()).ToBeHiddenAsync();
        await Expect(form.MessageRequiredError()).ToBeHiddenAsync();
    }
}
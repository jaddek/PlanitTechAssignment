using Microsoft.Playwright;
using TechnicalAssessmentTests.Pages;

namespace TechnicalAssessmentTests.Tests.Cases;

// Test case 2:
// 1. From the home page go to contact page
// 2. Populate mandatory fields
// 3. Click submit button
// 4. Validate successful submission message
// Note: Run this test 5 times to ensure 100% pass rate
public class ContactFormSuccessfulSubmit : AbstractCase
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
        var homePage = new HomePage(Page);

        await homePage.GoToAsync();

        Assert.True(await homePage.MenuComponent.TopLeftMenu.Contact.IsVisibleAsync(),
            "Contact link should be available");
        await homePage.MenuComponent.TopLeftMenu.Contact.ClickAsync();

        var contactPage = new ContactPage(Page);
        var form = contactPage.FormComponent;

        await Expect(form.Forename()).ToBeVisibleAsync();
        await Expect(form.Email()).ToBeVisibleAsync();
        await Expect(form.Message()).ToBeVisibleAsync();
        await Expect(form.Submit()).ToBeVisibleAsync();


        await form.Forename().FillAsync(forename);
        await form.Email().FillAsync(email);
        await form.Message().FillAsync(message);

        await contactPage.FormComponent.Submit().ClickAsync();

        /*
            Timeout should be set up to 18999ms
         
            var rt = ( Math.floor(Math.random()*1001) * Math.floor(Math.random()*10) + 1000);
            if(rt % 2) {
                rt += 9000;
            }
            popup.wait('Sending Feedback',$scope,(rt/1000));
         */
        await Expect(form.SuccessMessage())
            .ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = 19000 });
    }
}
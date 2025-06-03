using Microsoft.Playwright;
using TechnicalAssessmentTests.Components;
using TechnicalAssessmentTests.Components.Form;
using static Microsoft.Playwright.Assertions;

namespace TechnicalAssessmentTests.Pages;

public sealed class ContactPage : AbstractPage
{
    public static int DefaultTimeout => 19000;

    public MenuComponent MenuComponent { get; }
    public ContactForm FormComponent { get; }

    protected override string Fragment => "/";

    
    public ContactPage(IPage page) : base(page)
    {
        MenuComponent = new MenuComponent(Page, ".navbar-inner");
        FormComponent = new ContactForm(Page, "form");
    }
}

public class ContactPageAssertScenarioManager(ContactPage page)
{
    private readonly ContactPageAssertManager _assertManager = new(page);

    private async Task SubmitContactFormAsync()
    {
        await  page.FormComponent.Submit().ClickAsync();
    }
    
    private async Task PopulateFormDataAsync(
        string forename,
        string email,
        string message
    )
    {
        var form = page.FormComponent;

        await form.Forename().FillAsync(forename);
        await form.Email().FillAsync(email);
        await form.Message().FillAsync(message);
    }

    public async Task ValidateFormErrorMessagesGoneAfterValidPopulation(
        string forename,
        string email,
        string message
    )
    {
        await _assertManager.AssertFormVisibleAsync();
        await _assertManager.AssertFormRequiredInputsAndSubmitVisibleAsync();
        await SubmitContactFormAsync();

        await _assertManager.AssertFormRequiredInputsErrorMessagesVisibleAsync();
        
        await PopulateFormDataAsync(forename, email, message);
        await SubmitContactFormAsync();

        
        await _assertManager.AssertFormRequiredInputsErrorMessagesNotVisibleAsync();
    }

    public async Task ValidateFormSuccessSubmitAsync(
        string forename,
        string email,
        string message
    )
    {
        await _assertManager.AssertFormVisibleAsync();
        await _assertManager.AssertFormRequiredInputsAndSubmitVisibleAsync();

        await PopulateFormDataAsync(forename, email, message);
        await SubmitContactFormAsync();
        
        await _assertManager.AssertFormSubmittedAndShowSuccessMessage(ContactPage.DefaultTimeout);
    }
}

public sealed class ContactPageAssertManager(ContactPage page)
{
    public async Task AssertFormVisibleAsync()
    {
        await Expect(page.FormComponent.Node).ToBeVisibleAsync();
    }

    /**
     * Timeout should be set up to 18999ms

          var rt = ( Math.floor(Math.random()*1001) * Math.floor(Math.random()*10) + 1000);
          if(rt % 2) {
              rt += 9000;
          }
          popup.wait('Sending Feedback',$scope,(rt/1000));
     */
    public async Task AssertFormSubmittedAndShowSuccessMessage(int timeout)
    {
        await Expect(page.FormComponent.SuccessMessage())
            .ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions { Timeout = timeout });
    }

    public async Task AssertFormRequiredInputsAndSubmitVisibleAsync()
    {
        await Expect(page.FormComponent.Forename()).ToBeVisibleAsync();
        await Expect(page.FormComponent.Email()).ToBeVisibleAsync();
        await Expect(page.FormComponent.Message()).ToBeVisibleAsync();
        await Expect(page.FormComponent.Submit()).ToBeVisibleAsync();
    }

    public async Task AssertFormRequiredInputsErrorMessagesVisibleAsync()
    {
        await Expect(page.FormComponent.ForenameRequiredError()).ToBeVisibleAsync();
        await Expect(page.FormComponent.EmailRequiredError()).ToBeVisibleAsync();
        await Expect(page.FormComponent.MessageRequiredError()).ToBeVisibleAsync();
    }

    public async Task AssertFormRequiredInputsErrorMessagesNotVisibleAsync()
    {
        await Expect(page.FormComponent.ForenameRequiredError()).ToBeHiddenAsync();
        await Expect(page.FormComponent.EmailRequiredError()).ToBeHiddenAsync();
        await Expect(page.FormComponent.MessageRequiredError()).ToBeHiddenAsync();
    }
}
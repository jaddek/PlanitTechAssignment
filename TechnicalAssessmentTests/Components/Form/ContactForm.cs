using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace TechnicalAssessmentTests.Components.Form;

public class ContactForm(IPage page, string selector) : FormComponent(page, selector)
{
    private readonly IPage _page = page;
    private const string SuccessAlertSelector = ".alert-success";
    private const string SubmitLinkName = "Submit";
    private const string EmailTextboxName = "Email *";
    private const string MessageTextboxName = "Message *";
    private const string ForenameTextboxName = "Forename *";

    private const string EmailRequiredText = "Email is required";
    private const string EmailInvalidText = "Please enter a valid email";
    private const string MessageRequiredText = "Message is required";
    private const string ForenameRequiredText = "Forename is required";
    
    public ILocator SuccessMessage() =>
        _page.Locator(SuccessAlertSelector);

    public ILocator Submit() =>
        _page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = SubmitLinkName });

    public ILocator Email() =>
        _page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = EmailTextboxName });

    public ILocator EmailRequiredError(string text = EmailRequiredText) =>
        _page.GetByText(text);

    public ILocator EmailInvalidError(string text = EmailInvalidText) =>
        _page.GetByText(text);

    public ILocator Message() =>
        _page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = MessageTextboxName });

    public ILocator MessageRequiredError(string text = MessageRequiredText) =>
        _page.GetByText(text);

    public ILocator Forename() =>
        _page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = ForenameTextboxName });

    public ILocator ForenameRequiredError(string text = ForenameRequiredText) =>
        _page.GetByText(text);
}
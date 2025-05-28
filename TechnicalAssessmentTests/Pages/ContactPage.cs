using Microsoft.Playwright;
using TechnicalAssessmentTests.Components;
using TechnicalAssessmentTests.Components.Form;

namespace TechnicalAssessmentTests.Pages;

public sealed class ContactPage: AbstractPage
{
    public MenuComponent MenuComponent { get; }
    public ContactForm FormComponent { get; }
    
    protected override string Fragment => "/";
    
    public ContactPage(IPage page) : base(page)
    {
        MenuComponent = new MenuComponent(Page, "nav-collapse");
        FormComponent = new ContactForm(Page, "form");
    }
}
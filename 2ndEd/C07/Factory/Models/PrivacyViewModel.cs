using Microsoft.AspNetCore.Html;

namespace Factory.Models;

public class PrivacyViewModel
{
    public string? Title { get; set; }
    public HtmlString? Content { get; set; }
}

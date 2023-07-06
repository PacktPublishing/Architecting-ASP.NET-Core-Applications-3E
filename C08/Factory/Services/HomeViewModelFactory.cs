using Factory.Models;
using Microsoft.AspNetCore.Html;

namespace Factory.Services;

public interface IHomeViewModelFactory
{
    PrivacyViewModel CreatePrivacyViewModel();
}

public class HomeViewModelFactory : IHomeViewModelFactory
{
    public PrivacyViewModel CreatePrivacyViewModel() => new()
    {
        Title = "Privacy Policy (from IHomeViewModelFactory)",
        Content = new HtmlString("<p>Use this page to detail your site's privacy policy.</p>")
    };
}

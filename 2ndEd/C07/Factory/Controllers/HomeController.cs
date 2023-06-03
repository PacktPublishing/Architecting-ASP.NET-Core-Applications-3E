using Factory.Models;
using Factory.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Factory.Controllers;

public class HomeController : Controller
{
    private readonly IHomeViewModelFactory _homeViewModelFactory;
    public HomeController(IHomeViewModelFactory homeViewModelFactory)
    {
        _homeViewModelFactory = homeViewModelFactory ?? throw new ArgumentNullException(nameof(homeViewModelFactory));
    }

    public IActionResult Index([FromServices] HomePageViewModel viewModel)
    {
        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        var viewModel = _homeViewModelFactory.CreatePrivacyViewModel();
        return View(viewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

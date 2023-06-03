using Microsoft.AspNetCore.Mvc;
using Strategy.Models;
using Strategy.Services;
using System.Diagnostics;

namespace Strategy.Controllers;

public class HomeController : Controller
{
    private readonly IHomeService _homeService;
    public HomeController(IHomeService homeService)
    {
        _homeService = homeService ?? throw new ArgumentNullException(nameof(homeService));
    }

    public IActionResult Index()
    {
        var data = _homeService.GetHomePageData();
        var viewModel = new HomePageViewModel(data);
        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}


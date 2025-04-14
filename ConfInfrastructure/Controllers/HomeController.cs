using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ConfInfrastructure.Models;
using ConfInfrastructure.ViewModels;
using Microsoft.EntityFrameworkCore;
using ConfDomain.Model;

namespace ConfInfrastructure.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DbconappContext _context;

    public HomeController(ILogger<HomeController> logger, DbconappContext context)
    {
        _logger = logger;
        _context = context;
    }

    /*
    public IActionResult Index()
    {
        return View();
    }
    */
    
    public IActionResult Index()
    {
        var upcomingConferences = _context.Conferences
            .Where(c => c.Date >= DateTime.Today)
            .OrderBy(c => c.Date)
            .Take(5)
            .Select(c => new UpcomingConferenceViewModel
            {
                Title = c.Title,
                Place = c.Place,
                Date = c.Date,
                Description = c.Description.Length > 100
                    ? c.Description.Substring(0, 100) + "..."
                    : c.Description
            })
            .ToList();

        return View(upcomingConferences);
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

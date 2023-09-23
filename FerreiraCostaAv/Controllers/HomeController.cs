using FerreiraCostaAv.Data;
using FerreiraCostaAv.Interfaces;
using FerreiraCostaAv.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FerreiraCostaAv.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext dbContext;
    private readonly IUserService userService;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext, IUserService userService)
    {
      this._logger = logger;
      this.dbContext = dbContext;
      this.userService = userService;
    }

    public IActionResult Index()
    {
      return View();
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
}

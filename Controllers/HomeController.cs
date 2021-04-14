using L4_DAVH_AFPE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace L4_DAVH_AFPE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Developer(IFormCollection collection)
        {
            L4_DAVH_AFPE.Models.Data.Singleton.Instance.loginType = false;
            return RedirectToAction(nameof(Index), ("Tasks"));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProjectManager(IFormCollection collection)
        {
            L4_DAVH_AFPE.Models.Data.Singleton.Instance.loginType = true;
            return RedirectToAction(nameof(Index), ("Tasks"));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using L4_DAVH_AFPE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using L4_DAVH_AFPE.Models.Data;

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
            if(System.IO.File.Exists("./Database.txt"))
            {
                //lectura del archivo
                return View();
            }
            else
            {
                return RedirectToAction(nameof(Configuration));
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(IFormCollection collection)
        {
            return RedirectToAction(nameof(Index), ("Task"));
        }

        public IActionResult Configuration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Configuration(IFormCollection collection)
        {
            Singleton.Instance.PriorityTask = new BinaryHeap<string>(Singleton.Instance.heapCapacity);
            Singleton.Instance.Tasks = new HashTable<TaskModel, int>(Singleton.Instance.hashCapacity);
            return RedirectToAction(nameof(Index));
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

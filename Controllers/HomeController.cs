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
using System.IO;

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
                var lectorlinea = new StreamReader("./Database.txt");
                string line = lectorlinea.ReadToEnd();
                string[] obj = line.Split("\n");
                for(int i = 0; i < obj.Length; i++)
                {
                    int spacer = obj[i].IndexOf(":");
                    if (obj[i].Substring(0, spacer) == "heapCapacity")
                    {
                        Singleton.Instance.heapCapacity = Convert.ToInt32(obj[i].Substring(spacer + 1));
                        Singleton.Instance.PriorityTask = new BinaryHeap<string>(Singleton.Instance.heapCapacity);
                    }
                    if (obj[i].Substring(0, spacer) == "hashCapacity")
                    {
                        Singleton.Instance.hashCapacity = Convert.ToInt32(obj[i].Substring(spacer + 1));
                        Singleton.Instance.Tasks = new HashTable<TaskModel, int>(Singleton.Instance.hashCapacity);
                    }
                }
                lectorlinea.Close();    
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
            Singleton.Instance.user = collection["user"];
            Singleton.Instance.loginType = Convert.ToBoolean(collection["admin"]);
            if(Singleton.Instance.loginType)
            {
                Singleton.Instance.txt = "PROJECT MANAGER";
            }
            else
            {
                Singleton.Instance.txt = "DEVELOPER";
            }
            return RedirectToAction(nameof(Index), ("Task"));
        }

        public IActionResult Configuration()
        {
            return View();
        }
       

        [HttpPost]
        [ValidateAntiForgeryToken]
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

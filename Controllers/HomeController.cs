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
using DataStructures;
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
            //If the DATABASE text file exists, it will read the entire information to be loaded onto the system.
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
                        Singleton.Instance.PriorityTask = new Heap<string>(Singleton.Instance.heapCapacity);
                    }
                    if (obj[i].Substring(0, spacer) == "hashCapacity")
                    {
                        Singleton.Instance.hashCapacity = Convert.ToInt32(obj[i].Substring(spacer + 1));
                        Singleton.Instance.Tasks = new HashTable<TaskModel, int>(Singleton.Instance.hashCapacity);
                    }
                    if (obj[i].Substring(0, spacer) == "tasks")
                    {
                        string[] tasks = obj[i].Substring(spacer + 1).Split(";");
                        
                        for (int j = 0; j < tasks.Length; j++)
                        { 
                            string[] obj2 = tasks[j].Split(",");
                            if (obj2.Length == 6) {                             
                                var newTask = new TaskModel
                                {
                                    title = obj2[0],
                                    description = obj2[1],
                                    project = obj2[2],
                                    priority = Convert.ToInt32(obj2[3]),
                                    date = obj2[4],
                                    inCharge = obj2[5],
                                };
                                Singleton.Instance.PriorityTask.insertKey(newTask.title, newTask.priority);
                                Singleton.Instance.Tasks.Add(newTask, Singleton.Instance.keyGen(newTask.title));
                            }
                        }
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
                Singleton.Instance.txt = "Project Manager";
            }
            else
            {
                Singleton.Instance.txt = "Developer";
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
            Singleton.Instance.PriorityTask = new Heap<string>(Singleton.Instance.heapCapacity);
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

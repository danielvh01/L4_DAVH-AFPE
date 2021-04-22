using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using L4_DAVH_AFPE.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using L4_DAVH_AFPE.Models.Data;

namespace L4_DAVH_AFPE.Controllers
{
    public class TaskController : Controller
    {
        string session;
        private readonly IHostingEnvironment hostingEnvironment;
        public TaskController(IHostingEnvironment hostingEnvironment)
        {            
            session = "Database.txt";            
            this.hostingEnvironment = hostingEnvironment;
        }
        // GET: TaskController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TaskController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TaskController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaskController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var newTask = new TaskModel
                {
                    title = collection["title"],
                    description = collection["description"],
                    project = collection["project"],
                    priority = Convert.ToInt32(collection["priority"]),
                    date = collection["date"],
                };
                if (Singleton.Instance.Tasks.Get(newTask, Singleton.Instance.keyGen(newTask.title)) != default) {
                    
                }
                //COMPONER
                Singleton.Instance.PriorityTask.insertKey(newTask.title,newTask.priority);
                Singleton.Instance.Tasks.Add(newTask,Singleton.Instance.keyGen(newTask.date));

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TaskController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TaskController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TaskController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TaskController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Data()
        {

            StreamWriter file = new StreamWriter(session, true);
            file.Write(Singleton.Instance.database);
            file.Close();
            return View();
        }
    }
}

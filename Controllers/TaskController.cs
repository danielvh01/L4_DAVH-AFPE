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
            return View(Singleton.Instance.PriorityTask);
        }

        // GET: TaskController/Details/5
        public ActionResult Details(string value)
        {
            TaskModel task = Singleton.Instance.Tasks.Get(new TaskModel(value), Singleton.Instance.keyGen(value));
            return View(task);
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
                    inCharge = Singleton.Instance.user
                };
                //Poner un if de que si no se repite el titulo, ingrese
                if (Singleton.Instance.Tasks.Get(newTask,Singleton.Instance.keyGen(newTask.title)) == null)
                {
                    Singleton.Instance.PriorityTask.insertKey(newTask.title,newTask.priority);
                    Singleton.Instance.Tasks.Add(newTask,Singleton.Instance.keyGen(newTask.title));
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TaskController/Edit/5
        public ActionResult Edit(string value)
        {
            TaskModel task = Singleton.Instance.Tasks.Get(new TaskModel(value), Singleton.Instance.keyGen(value));
            return View(task);
        }

        // POST: TaskController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IFormCollection collection)
        {
            string value = collection["title"];
            if (collection["title"].Count > 1)
            {
                value = collection["title"][1];
            }
            TaskModel edited = Singleton.Instance.Tasks.Get(new TaskModel(value), Singleton.Instance.keyGen(value));
            if(Singleton.Instance.Tasks.Get(new TaskModel(collection["title"][0]), Singleton.Instance.keyGen(collection["title"][0])) == null)
            {
                Singleton.Instance.Tasks.Delete(edited, Singleton.Instance.keyGen(edited.title));
                Singleton.Instance.PriorityTask.Delete(value);
                edited.title = collection["title"][0];
                edited.priority = Convert.ToInt32(collection["priority"]);
                edited.description = collection["description"];
                edited.project = collection["project"];
                edited.date = collection["date"];
                Singleton.Instance.Tasks.Add(edited, Singleton.Instance.keyGen(edited.title));
                Singleton.Instance.PriorityTask.insertKey(edited.title, edited.priority);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["testmsg"] = "This title alredy exists, please try with another.";
                return View(edited);
            }
        }

        // GET: TaskController/Delete/5
        public ActionResult Delete(string value)
        {
            TaskModel task = Singleton.Instance.Tasks.Get(new TaskModel(value), Singleton.Instance.keyGen(value));
            return View(task);
        }

        // POST: TaskController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(TaskModel value, IFormCollection collection)
        {
            try
            {
                Singleton.Instance.Tasks.Delete(value, Singleton.Instance.keyGen(value.title));
                Singleton.Instance.PriorityTask.Delete(value.title);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Data()
        {
            Singleton.Instance.BuildData();
            StreamWriter file = new StreamWriter(session,false);
            file.Write(Singleton.Instance.database);
            file.Close();
            return RedirectToAction(nameof(Index));
        }
    }
}

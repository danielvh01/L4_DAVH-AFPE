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
            if (Singleton.Instance.loginType)
            {
                return View(Singleton.Instance.PriorityTask);
            }
            else
            {
                return View("DIndex", Singleton.Instance.PriorityTask);
            }
        }

        [HttpPost]
        public ActionResult Index(IFormCollection collection)
        {
            string filter = collection["search"];
            if(filter != "")
            {
                if (Singleton.Instance.loginType)
                {
                    return View(Singleton.Instance.PriorityTask.Search(x => x.CompareTo(filter)));
                }
                else
                {
                    return View("DIndex", Singleton.Instance.PriorityTask.Search(x => x.CompareTo(filter)));
                }
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: TaskController/Details/5
        public ActionResult CurrentTask()
        {
            if (Singleton.Instance.PriorityTask.Length() > 0 )
            {

                TaskModel task = new TaskModel();
                for (int i = 0; i < Singleton.Instance.PriorityTask.Length(); i++)
                {
                    string title = Singleton.Instance.PriorityTask.heapArray.Get(i).value;
                    task = Singleton.Instance.Tasks.Get(x => x.title.CompareTo(title), Singleton.Instance.keyGen(title));
                    if (task.inCharge == Singleton.Instance.user)
                    {
                        break;
                    }
                }
                if (task.inCharge == Singleton.Instance.user)
                {                    
                    if (task != default)
                    {
                        return View(task);
                    }
                    else
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    TempData["testmsg"] = "Wow! It seems that you do not have pending tasks!";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["testmsg"] = "Wow! It seems that you do not have pending tasks!";
                return RedirectToAction(nameof(Index));
            }
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
                if (Singleton.Instance.Tasks.Get(newTask, Singleton.Instance.keyGen(newTask.title)) == null)
                {
                    Singleton.Instance.PriorityTask.insertKey(newTask.title, newTask.priority);
                    Singleton.Instance.Tasks.Add(newTask, Singleton.Instance.keyGen(newTask.title));
                    Data();
                }
                else {
                    TempData["testmsg"] = "This title alredy exists, please try with another.";
                    return RedirectToAction(nameof(Index));
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
            Singleton.Instance.edit = Singleton.Instance.Tasks.Get(x => x.title.CompareTo(value), Singleton.Instance.keyGen(value));
            return View(Singleton.Instance.edit);
        }

        // POST: TaskController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IFormCollection collection)
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
            if (Singleton.Instance.edit.title != collection["title"])
            {
                if (Singleton.Instance.Tasks.Get(newTask, Singleton.Instance.keyGen(newTask.title)) == null)
                {
                    Singleton.Instance.Tasks.Delete(Singleton.Instance.edit, Singleton.Instance.keyGen(Singleton.Instance.edit.title));
                    Singleton.Instance.PriorityTask.Delete(Singleton.Instance.edit.title);
                    Singleton.Instance.PriorityTask.insertKey(newTask.title, newTask.priority);
                    Singleton.Instance.Tasks.Add(newTask, Singleton.Instance.keyGen(newTask.title));

                }
                else
                {
                    TempData["testmsg"] = "This title alredy exists, please try with another.";
                    return View(Singleton.Instance.edit);
                }
            }
            else 
            {
                Singleton.Instance.Tasks.Delete(Singleton.Instance.edit, Singleton.Instance.keyGen(Singleton.Instance.edit.title));
                Singleton.Instance.PriorityTask.Delete(Singleton.Instance.edit.title);
                Singleton.Instance.PriorityTask.insertKey(newTask.title, newTask.priority);
                Singleton.Instance.Tasks.Add(newTask, Singleton.Instance.keyGen(newTask.title));
            }                
            Data();
            return RedirectToAction(nameof(Index));
        }

        // GET: TaskController/Delete/5
        public ActionResult Delete(string value)
        {
            TaskModel task = Singleton.Instance.Tasks.Get(x => x.title.CompareTo(value), Singleton.Instance.keyGen(value));
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
                Data();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Done(TaskModel value)
        {
            Singleton.Instance.Tasks.Delete(value, Singleton.Instance.keyGen(value.title));
            Singleton.Instance.PriorityTask.Delete(value.title);
            Data();
            return RedirectToAction(nameof(Index));
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

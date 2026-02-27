using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission08_0404.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mission08_0404.Controllers
{
    public class HomeController : Controller
    {
        private ITaskRepository _repo;

        public HomeController(ITaskRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            var tasks = _repo.GetTasks();

            return View(tasks);
        }
        public IActionResult CompletedTasks()
        {
            var completedTasks = _repo.GetCompleteTasks();

            return View(completedTasks ?? new List<TaskItem>());
        }
        public IActionResult AddEditTasks(int? id)
        {
            ViewBag.Categories = new SelectList(_repo.Categories, "CatId", "CatName");

            TaskItem task;

            if (id == null)
            {
                task = new TaskItem()
                {
                    DueDate = DateOnly.FromDateTime(DateTime.Today)
                };
            }
            else
            {
                task = _repo.Tasks
                    .Single(t => t.TaskId == id);
            }

            return View(task);
        }
        
        [HttpPost]
        public IActionResult AddEditTasks(TaskItem task)
        {
            ViewBag.Categories = new SelectList(_repo.Categories, "CatId", "CatName");

            if (task.TaskId == 0)
            {
                _repo.AddTask(task);
            }
            else
            {
                _repo.UpdateTask(task);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CompleteTask(int id)
        {
            var task = _repo.Tasks
                .Single(t => t.TaskId == id);
            task.CompletedFlag = 1;
            _repo.UpdateTask(task);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteTask(int id)
        {
            var task = _repo.Tasks
                .Single(t => t.TaskId == id);

            return View(task);
        }
        [HttpPost]
        public IActionResult DeleteTask(TaskItem task)
        {
            _repo.DeleteTask(task);

            return RedirectToAction("Index");
        }
    }
}

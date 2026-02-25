using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission08_0404.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mission08_0404.Controllers
{
    public class HomeController : Controller
    {
        private readonly Mission08Context _context;

        public HomeController(Mission08Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var tasks = _context.Tasks
                .Include(t => t.Cat)
                .Include(t => t.Quadrant)
                .ToList();

            return View(tasks);
        }
        public IActionResult CompletedTasks()
        {
            var completedTasks = _context.Tasks
                .Include(t => t.Cat)
                .Include(t => t.Quadrant)
                .Where(t => t.CompletedFlag == 1)
                .ToList();

            return View(completedTasks ?? new List<TaskItem>());
        }
        public IActionResult AddEditTasks(int? id)
        {
            ViewBag.Categories = new SelectList(_context.Categories, "CatId", "CatName");

            if (id == null)
            {
                return View(new TaskItem());
            }

            var task = _context.Tasks.Find(id);
            return View(task);
        }
        
        [HttpPost]
        public IActionResult AddEditTasks(TaskItem task)
        {
            ViewBag.Categories = new SelectList(_context.Categories, "CatId", "CatName");

            if (task.TaskId == 0)
            {
                _context.Tasks.Add(task);
            }
            else
            {
                _context.Tasks.Update(task);
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

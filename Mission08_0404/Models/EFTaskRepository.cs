using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace Mission08_0404.Models
{
    public class EFTaskRepository : ITaskRepository
    {
        private Mission08Context _context;
        public EFTaskRepository(Mission08Context context)
        {
            _context = context;
        }

        public List<TaskItem> Tasks => _context.Tasks.ToList();
        public void AddTask(TaskItem task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }
        public void UpdateTask(TaskItem task)
        {
            _context.Tasks.Update(task);
            _context.SaveChanges();
        }
        public void DeleteTask(TaskItem task)
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }
        public List<TaskItem> GetTasks()
        {
            return _context.Tasks
                .Include(t => t.Cat)
                .Include(t => t.Quadrant)
                .ToList();
        }
        public List<TaskItem> GetCompleteTasks()
        {
            return _context.Tasks
                .Include(t => t.Cat)
                .Include(t => t.Quadrant)
                .Where(t => t.CompletedFlag == 1)
                .ToList();
        }
        public List<Category> Categories => _context.Categories.ToList();
        public List<Quadrant> Quadrants => _context.Quadrants.ToList();
    }
}

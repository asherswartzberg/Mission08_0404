namespace Mission08_0404.Models
{
    public interface ITaskRepository
    {
        List<TaskItem> Tasks { get; }
        public void AddTask(TaskItem task);
        public void UpdateTask(TaskItem task);
        public void DeleteTask(TaskItem task);
        public List<TaskItem> GetTasks();
        public List<TaskItem> GetCompleteTasks();
        List<Category> Categories { get; }
        List<Quadrant> Quadrants { get; }
    }
}

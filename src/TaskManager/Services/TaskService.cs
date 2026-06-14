using TaskManager.Models;
using TaskManager.Repositories;

namespace TaskManager.Services;

public class TaskService
{
    private readonly ITaskRepository _repository;
    private List<TaskItem> _tasks;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
        _tasks = _repository.LoadAll();
    }

    public TaskItem AddTask(string title, string description = "")
    {
        var id = _tasks.Count > 0 ? _tasks.Max(t => t.Id) + 1 : 1;
        var task = new TaskItem
        {
            Id = id,
            Title = title,
            Description = description,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };
        _tasks.Add(task);
        _repository.SaveAll(_tasks);
        return task;
    }

    public List<TaskItem> GetAll() => new List<TaskItem>(_tasks);

    public bool CompleteTask(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task == null) return false;
        task.IsCompleted = true;
        _repository.SaveAll(_tasks);
        return true;
    }

    public bool DeleteTask(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task == null) return false;
        _tasks.Remove(task);
        _repository.SaveAll(_tasks);
        return true;
    }
}

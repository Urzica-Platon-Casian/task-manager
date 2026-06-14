using System.Text.Json;
using TaskManager.Models;

namespace TaskManager.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly string _filePath;

    public TaskRepository(string filePath = "tasks.json")
    {
        _filePath = filePath;
    }

    public List<TaskItem> LoadAll()
    {
        if (!File.Exists(_filePath))
            return new List<TaskItem>();

        var json = File.ReadAllText(_filePath);
        if (string.IsNullOrWhiteSpace(json))
            return new List<TaskItem>();

        return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
    }

    public void SaveAll(List<TaskItem> tasks)
    {
        var json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
}

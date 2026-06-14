using TaskManager.Models;

namespace TaskManager.Repositories;

public interface ITaskRepository
{
    List<TaskItem> LoadAll();
    void SaveAll(List<TaskItem> tasks);
}

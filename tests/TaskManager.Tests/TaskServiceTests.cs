using TaskManager.Repositories;
using TaskManager.Services;
using Xunit;

namespace TaskManager.Tests;

public class TaskServiceTests : IDisposable
{
    private readonly string _tempFile;
    private readonly TaskService _service;

    public TaskServiceTests()
    {
        _tempFile = Path.GetTempFileName();
        _service = new TaskService(new TaskRepository(_tempFile));
    }

    public void Dispose() => File.Delete(_tempFile);

    [Fact]
    public void AddTask_ReturnsTaskWithCorrectTitle()
    {
        var task = _service.AddTask("Buy groceries");
        Assert.Equal("Buy groceries", task.Title);
    }

    [Fact]
    public void AddTask_AssignsIncrementalIds()
    {
        var t1 = _service.AddTask("Task 1");
        var t2 = _service.AddTask("Task 2");
        Assert.True(t2.Id > t1.Id);
    }

    [Fact]
    public void AddTask_StartsAsNotCompleted()
    {
        var task = _service.AddTask("New task");
        Assert.False(task.IsCompleted);
    }

    [Fact]
    public void GetAll_ReturnsAllAddedTasks()
    {
        _service.AddTask("Task A");
        _service.AddTask("Task B");
        Assert.Equal(2, _service.GetAll().Count);
    }

    [Fact]
    public void CompleteTask_MarksTaskAsCompleted()
    {
        var task = _service.AddTask("Finish report");
        var result = _service.CompleteTask(task.Id);
        Assert.True(result);
        Assert.True(_service.GetAll().First(t => t.Id == task.Id).IsCompleted);
    }

    [Fact]
    public void CompleteTask_ReturnsFalseForInvalidId()
    {
        Assert.False(_service.CompleteTask(999));
    }

    [Fact]
    public void DeleteTask_RemovesTask()
    {
        var task = _service.AddTask("Temp task");
        var result = _service.DeleteTask(task.Id);
        Assert.True(result);
        Assert.Empty(_service.GetAll());
    }

    [Fact]
    public void DeleteTask_ReturnsFalseForInvalidId()
    {
        Assert.False(_service.DeleteTask(999));
    }
}

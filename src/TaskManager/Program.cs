using TaskManager.Repositories;
using TaskManager.Services;

var service = new TaskService(new TaskRepository());

if (args.Length == 0)
{
    PrintHelp();
    return;
}

switch (args[0].ToLower())
{
    case "add":
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: taskmanager add <title> [description]");
            break;
        }
        var added = service.AddTask(args[1], args.Length > 2 ? args[2] : "");
        Console.WriteLine($"Added: [{added.Id}] {added.Title}");
        break;

    case "list":
        var all = service.GetAll();
        if (all.Count == 0) { Console.WriteLine("No tasks."); break; }
        foreach (var t in all)
            Console.WriteLine($"[{t.Id}] [{(t.IsCompleted ? "X" : " ")}] {t.Title}" +
                              (string.IsNullOrEmpty(t.Description) ? "" : $" — {t.Description}"));
        break;

    case "complete":
        if (args.Length < 2 || !int.TryParse(args[1], out var completeId))
        {
            Console.WriteLine("Usage: taskmanager complete <id>");
            break;
        }
        Console.WriteLine(service.CompleteTask(completeId)
            ? $"Task {completeId} marked as completed."
            : $"Task {completeId} not found.");
        break;

    case "delete":
        if (args.Length < 2 || !int.TryParse(args[1], out var deleteId))
        {
            Console.WriteLine("Usage: taskmanager delete <id>");
            break;
        }
        Console.WriteLine(service.DeleteTask(deleteId)
            ? $"Task {deleteId} deleted."
            : $"Task {deleteId} not found.");
        break;

    default:
        PrintHelp();
        break;
}

static void PrintHelp()
{
    Console.WriteLine("TaskManager CLI");
    Console.WriteLine("  add <title> [description]  Add a new task");
    Console.WriteLine("  list                        List all tasks");
    Console.WriteLine("  complete <id>               Mark task as completed");
    Console.WriteLine("  delete <id>                 Delete a task");
}

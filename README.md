# TaskManager CLI

A simple command-line task manager written in C# (.NET 10). Tasks are stored locally in a `tasks.json` file.

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

## Build

```bash
dotnet build --configuration Release
```

## Usage

```bash
dotnet run --project src/TaskManager -- <command>
```

Or after publishing:

```bash
./TaskManager <command>
```

### Commands

| Command | Description |
|---|---|
| `add <title> [description]` | Add a new task |
| `list` | List all tasks |
| `complete <id>` | Mark a task as completed |
| `delete <id>` | Delete a task |

### Examples

```bash
# Add tasks
dotnet run --project src/TaskManager -- add "Buy milk" "From the store"
dotnet run --project src/TaskManager -- add "Send email"

# List tasks
dotnet run --project src/TaskManager -- list
# [1] [ ] Buy milk — From the store
# [2] [ ] Send email

# Complete a task
dotnet run --project src/TaskManager -- complete 1
# Task 1 marked as completed.

# Delete a task
dotnet run --project src/TaskManager -- delete 2
# Task 2 deleted.
```

## Running Tests

```bash
dotnet test --configuration Release
```

## CI/CD

Every push to `main` triggers the GitHub Actions pipeline which:

1. Restores NuGet dependencies
2. Builds the solution
3. Runs all unit tests
4. Runs static code analysis (Roslyn analyzers)
5. Publishes a self-contained artifact (downloadable from the Actions tab)

## Project Structure

```
src/
  TaskManager/
    Models/         TaskItem model
    Repositories/   JSON file persistence (ITaskRepository + TaskRepository)
    Services/       Business logic (TaskService)
    Program.cs      CLI entry point
tests/
  TaskManager.Tests/
    TaskServiceTests.cs   xUnit tests for TaskService
.github/
  workflows/
    ci.yml          GitHub Actions pipeline
```

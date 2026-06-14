# TaskManager CLI — Ghid de demo

## Cum e structurat codul

Proiectul e împărțit în 3 straturi. Fiecare strat are o singură responsabilitate și nu știe cum funcționează celelalte.

**1. Model — `TaskItem.cs`**

Definește cum arată un task: ID, titlu, descriere, dacă e completat și când a fost creat. Nu face nimic, nu calculează nimic — e doar structura datei.

**2. Repository — `TaskRepository.cs` + `ITaskRepository.cs`**

Responsabil exclusiv cu citirea și scrierea pe disc. `LoadAll()` citește `tasks.json` și returnează lista de task-uri. `SaveAll()` o serializează înapoi în JSON. Interfața `ITaskRepository` există ca să poți înlocui implementarea în teste fără să atingi logica de business — în loc de fișier real, testele folosesc un fișier temporar.

**3. Service — `TaskService.cs`**

Aici e toată logica. La pornire primește un repository și încarcă lista în memorie (`_tasks`). Orice operație (adăugare, completare, ștergere) se face pe lista din memorie, după care se salvează pe disc. ID-ul nou e întotdeauna `max(id-uri existente) + 1`, sau 1 dacă lista e goală.

**4. Program.cs**

Entry point-ul. Citește primul argument din linie de comandă (`args[0]`) și îl trimite la service-ul potrivit. Nu conține logică — doar rutează comenzile.

**5. Testele — `TaskServiceTests.cs`**

Fiecare test pornește cu un fișier temporar gol și un service fresh. La final, `Dispose()` șterge fișierul. Testele verifică comportamentul service-ului: că un task adăugat are titlul corect, că ID-urile cresc, că `complete` pe un ID inexistent returnează `false`, etc. Nu testează `Program.cs` și nici `TaskRepository` direct — logica de business e izolată în service.

**6. CI/CD — `ci.yml`**

La fiecare push pe `main`, GitHub pornește un container Ubuntu, instalează .NET 10, și rulează în ordine: restore → build → teste → analiză statică → publish. Dacă oricare pas pică, tot pipeline-ul se oprește și push-ul e marcat cu roșu. Dacă totul trece, artefactul (executabilul compilat) e uploadat și disponibil de descărcat din tab-ul Actions.

---

## Demo pas cu pas

### Pasul 1 — Pornire fără argumente

```bash
dotnet run --project src/TaskManager
```

**Output:**
```
TaskManager CLI
  add <title> [description]  Add a new task
  list                        List all tasks
  complete <id>               Mark task as completed
  delete <id>                 Delete a task
```

Aplicația nu face nimic dacă nu primește o comandă — afișează help-ul și se închide.

---

### Pasul 2 — Adaugă câteva task-uri

```bash
dotnet run --project src/TaskManager -- add "Scrie documentatia" "Pana vineri"
dotnet run --project src/TaskManager -- add "Trimite email profesorului"
dotnet run --project src/TaskManager -- add "Review cod"
```

**Output:**
```
Added: [1] Scrie documentatia
Added: [2] Trimite email profesorului
Added: [3] Review cod
```

ID-urile se asignează automat, incremental. Acum există un fișier `tasks.json` în directorul proiectului.

---

### Pasul 3 — Listează task-urile

```bash
dotnet run --project src/TaskManager -- list
```

**Output:**
```
[1] [ ] Scrie documentatia — Pana vineri
[2] [ ] Trimite email profesorului
[3] [ ] Review cod
```

`[ ]` = necompletat. Descrierea apare după `—` doar dacă există.

---

### Pasul 4 — Marchează un task ca finalizat

```bash
dotnet run --project src/TaskManager -- complete 1
```

**Output:**
```
Task 1 marked as completed.
```

Listează din nou ca să confirmi:

```bash
dotnet run --project src/TaskManager -- list
```

```
[1] [X] Scrie documentatia — Pana vineri
[2] [ ] Trimite email profesorului
[3] [ ] Review cod
```

`[X]` înseamnă completat.

---

### Pasul 5 — Șterge un task

```bash
dotnet run --project src/TaskManager -- delete 3
```

**Output:**
```
Task 3 deleted.
```

---

### Pasul 6 — Testează cazurile de eroare

```bash
dotnet run --project src/TaskManager -- complete 999
dotnet run --project src/TaskManager -- delete 999
dotnet run --project src/TaskManager -- add
```

**Output:**
```
Task 999 not found.
Task 999 not found.
Usage: taskmanager add <title> [description]
```

Aplicația nu crăpă — gestionează input-ul invalid și afișează un mesaj clar.

---

### Pasul 7 — Rulează testele

```bash
dotnet test --configuration Release --verbosity normal
```

**Output:**
```
Passed  AddTask_ReturnsTaskWithCorrectTitle
Passed  AddTask_AssignsIncrementalIds
Passed  AddTask_StartsAsNotCompleted
Passed  GetAll_ReturnsAllAddedTasks
Passed  CompleteTask_MarksTaskAsCompleted
Passed  CompleteTask_ReturnsFalseForInvalidId
Passed  DeleteTask_RemovesTask
Passed  DeleteTask_ReturnsFalseForInvalidId

Total tests: 8  Passed: 8
```

---

### Pasul 8 — Verifică persistența

Închide terminalul, redeschide-l și rulează:

```bash
dotnet run --project src/TaskManager -- list
```

Task-urile sunt acolo. Datele nu se pierd — sunt salvate în `tasks.json` după fiecare operație.

---

### Pasul 9 — Verifică CI/CD pe GitHub

Deschide `https://github.com/Urzica-Platon-Casian/task-manager/actions` și apasă pe ultimul run. Vei vedea toate pașele pipeline-ului cu verde. Jos pe pagina run-ului e secțiunea **Artifacts** — acolo e executabilul compilat, downloadabil direct.

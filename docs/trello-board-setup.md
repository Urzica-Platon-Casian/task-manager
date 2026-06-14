# Agile Backlog – Setup Trello Board

## Structura Board

Creeaza un board pe https://trello.com cu numele **"TaskManager CLI"** si 4 coloane:

1. **Product Backlog** – toate user stories neincepute
2. **Sprint Backlog** – user stories selectate pentru sprintul curent
3. **In Progress** – task-uri in lucru
4. **Done** – task-uri finalizate

---

## User Stories (carduri in Product Backlog)

### Sprint 1 – Core Functionality

**US-01**: Adaugare task
> Ca utilizator, vreau sa adaug un task cu titlu si descriere, astfel incat sa imi pot urmari activitatile.
- Criteriu de acceptanta: Comanda `taskmanager add "Titlu" "Descriere"` salveaza task-ul si afiseaza ID-ul asignat.

**US-02**: Listare task-uri
> Ca utilizator, vreau sa vad toate task-urile existente, astfel incat sa am o imagine de ansamblu asupra workload-ului.
- Criteriu de acceptanta: Comanda `taskmanager list` afiseaza toate task-urile cu ID, status si titlu.

**US-03**: Completare task
> Ca utilizator, vreau sa marchez un task ca finalizat, astfel incat sa pot urmari progresul.
- Criteriu de acceptanta: Comanda `taskmanager complete 1` marcheaza task-ul cu ID 1 ca [X].

**US-04**: Stergere task
> Ca utilizator, vreau sa sterg un task irelevant, astfel incat lista sa ramana curata.
- Criteriu de acceptanta: Comanda `taskmanager delete 1` sterge task-ul si confirma operatiunea.

**US-05**: Persistenta datelor
> Ca utilizator, vreau ca task-urile sa fie salvate intre sesiuni, astfel incat sa nu pierd datele la inchiderea aplicatiei.
- Criteriu de acceptanta: Task-urile sunt prezente la repornirea aplicatiei.

### Sprint 2 – Quality & Infrastructure

**US-06**: Unit testing
> Ca developer, vreau sa am unit tests pentru logica de business, astfel incat sa previn regresia.
- Criteriu de acceptanta: Minimum 7 teste xUnit ruleaza cu succes.

**US-07**: Continuous Integration
> Ca developer, vreau ca orice push pe `main` sa triggereaze build si teste automat, astfel incat sa detectez erorile rapid.
- Criteriu de acceptanta: GitHub Actions ruleaza build + teste la fiecare push.

**US-08**: Static code analysis
> Ca developer, vreau ca Roslyn analyzers sa fie activi, astfel incat sa mentin calitatea codului.
- Criteriu de acceptanta: Build-ul in CI include analiza statica fara erori.

**US-09**: Deployment artifact
> Ca developer, vreau ca CI sa publice un artifact descarcabil, astfel incat sa pot distribui aplicatia.
- Criteriu de acceptanta: GitHub Actions incarca artefactul `task-manager-release` dupa fiecare build reusit.

---

## Configurare Labels (optionale)
- `bug` – rosu
- `feature` – verde
- `infrastructure` – albastru
- `documentation` – galben

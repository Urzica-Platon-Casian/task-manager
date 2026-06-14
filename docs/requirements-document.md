# Requirements Document — TaskManager CLI

**Versiune:** 1.0  
**Data:** 2026-06-14  
**Autor:** Casian Urzica-Platon

---

## 1. Descriere generala

TaskManager CLI este o aplicatie de consola scrisa in C# care permite gestionarea unei liste de task-uri. Utilizatorul interactioneaza cu aplicatia prin comenzi simple in terminal. Datele sunt salvate local intr-un fisier JSON, fara dependente externe (baza de date, servicii cloud etc.).

Scopul proiectului este demonstrarea unei infrastructuri tehnice complete: cod organizat pe straturi, unit testing, CI/CD prin GitHub Actions si analiza statica a codului.

---

## 2. Stakeholders

| Rol | Descriere |
|-----|-----------|
| Utilizator final | Persoana care ruleaza aplicatia din terminal |
| Developer | Responsabil cu implementarea si intretinerea codului |
| Profesor / Evaluator | Verifica indeplinirea cerintelor proiectului |

---

## 3. Cerinte functionale

### FR-01 — Adaugare task
Utilizatorul poate adauga un task specificand un titlu (obligatoriu) si o descriere (optionala). Aplicatia asigneaza automat un ID numeric unic, incremental. Task-ul nou creat are statusul necompletat.

**Comanda:** `taskmanager add <titlu> [descriere]`  
**Output:** `Added: [<id>] <titlu>`

### FR-02 — Listare task-uri
Utilizatorul poate vedea toate task-urile existente. Fiecare rand afiseaza ID-ul, statusul (`[ ]` sau `[X]`), titlul si descrierea (daca exista). Daca lista e goala, aplicatia afiseaza un mesaj corespunzator.

**Comanda:** `taskmanager list`

### FR-03 — Completare task
Utilizatorul poate marca un task ca finalizat prin ID. Aplicatia confirma operatiunea sau anunta ca ID-ul nu exista.

**Comanda:** `taskmanager complete <id>`

### FR-04 — Stergere task
Utilizatorul poate sterge un task prin ID. Aplicatia confirma stergerea sau anunta ca ID-ul nu exista.

**Comanda:** `taskmanager delete <id>`

### FR-05 — Persistenta
Task-urile sunt salvate in fisierul `tasks.json` din directorul de executie. La fiecare pornire, aplicatia incarca automat datele existente.

---

## 4. Cerinte non-functionale

| ID | Cerinta | Detaliu |
|----|---------|---------|
| NFR-01 | Portabilitate | Ruleaza pe Windows, Linux si macOS (.NET 10) |
| NFR-02 | Testabilitate | Logica de business e separata de I/O printr-o interfata (`ITaskRepository`), testabila independent |
| NFR-03 | Calitate cod | Roslyn analyzers activi in build; zero erori de analiza statica |
| NFR-04 | CI/CD | Orice push pe `main` triggereaza automat build, teste si publicare artifact |
| NFR-05 | Performanta | Toate operatiunile se executa in sub 100ms pe liste de pana la 1000 task-uri |

---

## 5. Constrangeri

- Limbaj: C# (.NET 10)
- Interfata: CLI (fara GUI)
- Stocare: fisier JSON local
- Source control: GitHub
- CI/CD: GitHub Actions

---

## 6. Criterii de acceptanta

- [ ] Toate comenzile (add, list, complete, delete) produc output-ul corect
- [ ] Comenzile cu argumente invalide sau ID-uri inexistente sunt tratate fara crash
- [ ] Datele persista intre rulari consecutive
- [ ] Toate cele 8 unit tests trec
- [ ] Pipeline-ul GitHub Actions ruleaza verde pe fiecare push pe `main`
- [ ] Artefactul publicat e disponibil in tab-ul Actions

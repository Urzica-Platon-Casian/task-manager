# Requirements Document – TaskManager CLI

**Project**: TaskManager CLI  
**Version**: 1.0  
**Date**: 2026-06-14  
**Author**: Platon Casian  

---

## 1. Project Overview

TaskManager CLI este o aplicatie de linie de comanda scrisa in C# care permite utilizatorilor sa gestioneze o lista de task-uri. Task-urile sunt persistate local intr-un fisier JSON.

## 2. Stakeholders

| Rol | Responsabilitate |
|-----|-----------------|
| Product Owner | Defineste si prioritizeaza cerintele |
| Developer | Implementeaza functionalitatea |
| QA | Valideaza comportamentul prin unit tests |

## 3. Functional Requirements

### FR-01: Adaugare task
- Utilizatorul poate adauga un task specificand un titlu (obligatoriu) si o descriere (optionala).
- Sistemul asigneaza automat un ID unic, incremental.
- Task-ul nou creat are statusul `IsCompleted = false`.

### FR-02: Listare task-uri
- Utilizatorul poate vizualiza toate task-urile existente.
- Fiecare task afisaza: ID, status (completat/necompletat), titlu si descriere.
- Daca nu exista task-uri, sistemul afiseaza un mesaj informativ.

### FR-03: Completare task
- Utilizatorul poate marca un task ca finalizat specificand ID-ul sau.
- Sistemul confirma operatiunea sau anunta ca ID-ul nu exista.

### FR-04: Stergere task
- Utilizatorul poate sterge un task specificand ID-ul sau.
- Sistemul confirma stergerea sau anunta ca ID-ul nu exista.

### FR-05: Persistenta datelor
- Task-urile sunt salvate intr-un fisier `tasks.json` in directorul de executie.
- La fiecare pornire a aplicatiei, task-urile existente sunt incarcate automat.

## 4. Non-Functional Requirements

| ID | Cerinta | Detalii |
|----|---------|---------|
| NFR-01 | Performanta | Operatiunile pe lista de task-uri se executa in sub 100ms |
| NFR-02 | Portabilitate | Aplicatia ruleaza pe Windows, Linux si macOS (.NET 8) |
| NFR-03 | Testabilitate | Logica de business este separata de I/O si testabila prin unit tests |
| NFR-04 | Calitate cod | Codul trece de Roslyn static analyzers fara erori |
| NFR-05 | CI/CD | Orice push pe `main` triggereaza build automat, teste si publicare artifact |

## 5. Constraints

- Limbaj: C# (.NET 8)
- Source control: GitHub
- CI/CD: GitHub Actions
- Stocare: fisier JSON local (fara baza de date)
- Interfata: CLI (fara GUI)

## 6. Acceptance Criteria

- [ ] Toate comenzile (add, list, complete, delete) functioneaza corect
- [ ] Unit tests acopera toate scenariile de business (inclusiv cazuri eronate)
- [ ] GitHub Actions pipeline ruleaza cu succes pe fiecare push
- [ ] Static code analysis nu raporteaza erori
- [ ] Artifact-ul publicat este downloadabil din GitHub Actions

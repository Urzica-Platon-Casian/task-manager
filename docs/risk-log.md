# Risk Log — TaskManager CLI

**Versiune:** 1.0  
**Data:** 2026-06-14

Scala: Probabilitate si Impact se noteaza de la 1 (scazut) la 3 (inalt). Scorul = P × I.

---

| ID | Descriere | P | I | Scor | Actiune |
|----|-----------|:-:|:-:|:----:|---------|
| R-01 | Fisierul `tasks.json` se poate corupe daca aplicatia se inchide fortat in timpul scrierii | 1 | 2 | 2 | `File.WriteAllText` scrie atomic pe majoritatea sistemelor de operare — riscul e acceptabil la scara actuala |
| R-02 | Pachetele NuGet pot contine vulnerabilitati de securitate | 1 | 3 | 3 | Se folosesc versiuni stabile LTS; GitHub Dependabot poate fi activat pentru alerte automate |
| R-03 | Pipeline-ul CI poate esua daca GitHub Actions isi schimba versiunea implicita de runner | 2 | 2 | 4 | Versiunea .NET e fixata explicit in `ci.yml` (`10.0.x`); se monitorizeaza anunturile de deprecare |
| R-04 | Cerinte descoperite tarziu care necesita refactorizari majore | 2 | 2 | 4 | Scope-ul proiectului e intentionat redus; backlog-ul Trello permite reprioretizare rapida |
| R-05 | Depasirea timpului alocat pentru implementare | 2 | 3 | 6 | Functionalitate minima definita clar de la inceput; sprinturi de o saptamana cu review la final |
| R-06 | Incompatibilitate intre versiunea .NET locala si cea din CI | 2 | 2 | 4 | Ambele folosesc .NET 10; un fisier `global.json` poate fixa versiunea SDK daca apar divergente |
| R-07 | Modificari breaking in xUnit sau alte dependente de test | 1 | 2 | 2 | Versiunile pachetelor sunt fixate in `.csproj`; upgrade-urile se fac explicit, nu automat |

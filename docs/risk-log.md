# Risk Log – TaskManager CLI

**Project**: TaskManager CLI  
**Version**: 1.0  
**Date**: 2026-06-14  

---

| ID | Risc | Probabilitate | Impact | Scor | Strategie de mitigare | Status |
|----|------|:---:|:---:|:---:|---|---|
| R-01 | Coruptia fisierului `tasks.json` din cauza unui crash in timpul scrierii | Scazut | Mediu | 4 | Scriere atomica: serialize in string, apoi `File.WriteAllText` (operatie atomica pe majoritatea OS-urilor) | Mitigat |
| R-02 | Dependinte NuGet cu vulnerabilitati de securitate | Scazut | Inalt | 6 | Folosire versiuni LTS stabile; GitHub Dependabot poate fi activat pentru alerte automate | Deschis |
| R-03 | GitHub Actions pipeline esueaza din cauza schimbarii versiunii de .NET | Mediu | Mediu | 6 | Specificarea explicita a versiunii .NET (`8.0.x`) in `ci.yml`; monitorizare anunturi EOL | Mitigat |
| R-04 | Cerinte incomplete sau ambigue identificate tarziu | Mediu | Mediu | 6 | Cerinte validate inainte de implementare; backlog Trello permite reprioretizare rapida | Mitigat |
| R-05 | Conflict de merge in repository daca mai multi contributori lucreaza simultan | Scazut | Scazut | 2 | Lucru pe branch-uri feature separate; PR review inainte de merge in `main` | Deschis |
| R-06 | Depasirea timpului alocat proiectului | Mediu | Inalt | 8 | Scope intentionat minim; functionalitate redusa; sprint-uri scurte de 1 saptamana | Monitorizat |
| R-07 | Incompatibilitate intre versiunea .NET de pe masina locala si cea din CI | Mediu | Mediu | 6 | `global.json` poate fixa versiunea SDK; CI foloseste aceeasi versiune declarata in `.csproj` | Deschis |

---

**Scala probabilitate**: Scazut (1) / Mediu (2) / Inalt (3)  
**Scala impact**: Scazut (1) / Mediu (2) / Inalt (3)  
**Scor** = Probabilitate × Impact (max 9)

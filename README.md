# NewgenSIMS (New Generation Security Incident Management System)

[![.NET](https://img.shields.io/badge/.NET-8.0-blue)](https://dotnet.microsoft.com/) 
[![Entity Framework Core](https://img.shields.io/badge/Entity%20Framework%20Core-8.0-green)](https://docs.microsoft.com/en-us/ef/) 
[![Redis](https://img.shields.io/badge/Redis-6.0-red)](https://redis.io/) 

---

## Projektübersicht

SIMS ist ein Security Incident Management System zur Verwaltung von sicherheitsrelevanten Vorfällen in IT-Systemen. Es ermöglicht Benutzern, Vorfälle zu erfassen, zu aktualisieren, zu eskalieren und zu protokollieren. Das System unterstützt Benutzerrollen wie Administratoren und normale Benutzer.

---

## Features
- **Benutzerverwaltung**: Hinzufügen, Bearbeiten und Deaktivieren von Benutzern.
- **Vorfallmanagement**: Erstellen, Bearbeiten, Schließen und Eskalieren von IT-Vorfällen.
- **Log-System**: Protokollierung von Aktionen und Ereignissen.
- **Rollenbasierte Zugriffssteuerung**: Administratoren vs. Benutzer.
- **Dockerized**: Einfache Bereitstellung mit Docker-Containern.

---

## Programmfunktionalität

Das Security Incident Management System (SIMS) bietet folgende Kernfunktionalitäten:

### 1. **Benutzerverwaltung**
- **Benutzer hinzufügen**: Administratoren können neue Benutzer mit spezifischen Rollen (z. B. "Admin", "User") anlegen.
- **Benutzer bearbeiten**: Aktualisieren von Benutzerinformationen wie Benutzernamen oder Rollen.
- **Benutzer deaktivieren**: Deaktivierung von Benutzerkonten, um deren Zugriff zu sperren.

### 2. **Vorfallmanagement**
- **Vorfall hinzufügen**: Erstellen von sicherheitsrelevanten Vorfällen mit Details wie Titel, Beschreibung, Schweregrad und zugewiesenem Bearbeiter.
- **Vorfall bearbeiten**: Aktualisierung bestehender Vorfälle, einschließlich Änderung des Status oder Eskalation.
- **Vorfall eskalieren**: Weiterleiten eines Vorfalls an den nächsthöheren Verantwortlichen oder ein anderes Eskalationslevel.
- **Vorfall schließen**: Abschließen eines Vorfalls nach der Lösung.

### 3. **Protokollierung**
- **Aktionslogs**: Jede Aktion im System wird automatisch protokolliert, z. B. das Erstellen, Bearbeiten oder Löschen eines Vorfalls.
- **Log-Level**: Einteilung der Logs in verschiedene Stufen wie "Info", "Warning" und "Error" zur besseren Übersicht.

### 4. **Rollenbasierte Zugriffssteuerung**
- **Administratoren**: Haben vollständige Kontrolle über Benutzerverwaltung und Vorfallmanagement.
- **Benutzer**: Können nur ihnen zugewiesene Vorfälle bearbeiten oder eigene Logs einsehen.

### 5. **Integration mit Redis und SQL-Datenbank**
- **Redis**: Verwendet für schnelles Caching und Session-Management.
- **SQL-Datenbank**: Persistente Speicherung von Vorfällen, Benutzern und Logs mit relationaler Datenstruktur.

### 6. **Docker-Unterstützung**
- Das gesamte System ist containerisiert und kann mit `docker-compose` einfach bereitgestellt werden.

---

## Systemarchitektur

Das System besteht aus folgenden Komponenten:
- **API**: RESTful-Endpunkte für Benutzer und Vorfälle.
- **Datenbank**: SQL-Datenbank zur Speicherung von Vorfällen, Benutzerinformationen und Logs.
- **Redis**: Session- und Cache-Management.
- **Docker**: Containerisierte Bereitstellung.

---

## Technische Details

- **Framework**: ASP.NET Core 8.0
- **Datenbank**: SQL Server und Redis
- **Architektur**: Microservices mit Docker-Containern
- **Programmiersprache**: C#

---

## Installation und Ausführung

### Voraussetzungen
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/)
- Redis und SQL Server

### Schritte
1. **Code klonen**:
   ```bash
   git clone https://github.com/vl43den/newgensims
   cd SIMS
   ```
2. **Docker-Container starten**:
   ```bash
   docker-compose up --build
   ```
3. **API starten**:
   ```bash
   dotnet run --project ./WebApp oder dotnet run --project ./UserApi
   ```

---

## Roadmap

- [x] API zur Benutzer- und Vorfallverwaltung
- [x] Redis-Integration für Sessions
- [x] SQL-Datenbank für Vorfallmanagement
- [X] Frontend-Integration
- [ ] Erweiterte Log-Analyse
- [ ] Unit-Tests

---

## Mitwirkende
- **Vladan**
- **Patrick**
- **Jakob**
- **Martin**

---

## Lizenz

Dieses Projekt steht unter der [MIT-Lizenz](LICENSE).

---

## Diagramme

### ER-Diagramm
![ER-Diagramm](https://via.placeholder.com/400?text=ER-Diagram)

### Klassendiagramm
![Klassendiagramm](https://via.placeholder.com/400?text=Klassendiagramm)

---

## Build-Status

[![Build](https://img.shields.io/badge/build-passing-brightgreen)](https://github.com/your-repo-link/actions)

---

## Ergebnisse von SAST (Static Application Security Testing)
- **Tool verwendet**: [Semgrep](https://semgrep.dev/)
- **Ergebnisse**:
  - TODO
  - TODO

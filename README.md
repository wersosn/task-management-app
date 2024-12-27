Version: [Polish](#Aplikacja-do-zarządzania-notatkami-i-zadaniami) | [English](#Notes-and-tasks-management-app)

# Aplikacja do zarządzania notatkami i zadaniami
## Spis treści
 - [Zespół](#zespół)
 - [Opis projektu](#opis-projektu)
 - [Funkcjonalności](#funkcjonalności)
 - [Użyte wzorce projektowe](#użyte-wzorce-projektowe)
 - [Użyte technologie](#użyte-technologie)
 - [Wymagania](#wymagania)
 - [Instrukcja użytkowania](#instrukcja-użytkowania)
   
## Zespół
Projekt został wykonany przez poniżej wymienione osoby:
 - [Ing862](https://github.com/Ing862) -
 - [wersosn](https://github.com/wersosn) - zaimplementowała 4 wzorce projektowe - Builder, Command, Template Method oraz Composite + pracowała nad widokami
 - [Gaspek](https://github.com/Gaspek) -
 - X

## Opis projektu
Aplikacja umożliwia użytkownikowi tworzenie, edytowanie i organizowanie notatek oraz list zadań. Notatki i zadania można opatrywać tagami (np. „praca”, „pomysł”) lub przypisywać do kategorii (np. „dom”, „projekt”). Zadania można oznaczać jako wykonane i nadawać im priorytety (np. „wysoki”, „niski”) oraz termin realizacji. Aplikacja obsługuje wyszukiwanie po słowach kluczowych, grupowanie według tagów lub kategorii, sortowanie według terminów lub priorytetów oraz generowanie raportów o zbliżających się terminach (np. „na najbliższy tydzień”) i podsumowań o wykonanych i zaległych zadaniach.

## Funkcjonalności
Poniżej zostały wypisane wszystkie funkcjonalności naszego projektu:
- Tworzenie, edytowanie, usuwanie oraz organizowanie notatek i zadań;
- Oznaczanie priorytetu zadań do wykonania;
- Ustalanie deadline’u dla zadań;
- Przypisywanie tagów oraz kategorii;
- Wyszukiwanie i sortowanie zadań oraz notatek;
- Grupowanie notatek i zadań;
- Generowanie raportów oraz podsumowań w formacie .txt, .docx, .pdf;

## Użyte wzorce projektowe
- Singleton
- Builder - do tworzenia zadań oraz notatek
- Command - do obsługi operacji na zadaniach i notatkach
- Composite - do stworzenia hierarchii kategorii oraz tagów
- Template method - do generowania raportów i podsumowań
- MVC/MVVM
  
## Użyte technologie
- C#
- WPF
- Visual Studio 2022
  
## Wymagania
- .Net 6+
- Visual Studio 2022
- Biblioteki: PdfSharp, DocumentFormat.OpenXml

## Instrukcja użytkowania
Aby uruchomić ten projekt należy sklonować to repozytorium:
```bash
git clone https://github.com/Ing862/SuperZTP-projekt
```
Następnie należy zainstalować następujące biblioteki:
```bash
# Obsługa plików .pdf
Install-Package PdfSharp
```
oraz 
```bash
# Obsługa plików .docx
Install-Package DocumentFormat.OpenXml
```

---
# Notes and tasks management app
## Table of Contents
 - [Team](#team)
 - [Project details](#project-details)
 - [Functionalities](#functionalities)
 - [Design patterns used](#design-patterns-used)
 - [Tech stack](#tech-stack)
 - [Requirements](#requirements)
 - [How to use](#how-to-use)

## Team
The project was implemented by the following people:
 - [Ing862](https://github.com/Ing862) -
 - [wersosn](https://github.com/wersosn) - she implemented 4 design patterns - Builder, Command, Template Method and Composite + worked on views
 - [Gaspek](https://github.com/Gaspek) -
 - X

## Project details
The application allows the user to create, edit, and organize notes and to-do lists. Notes and tasks can be tagged (e.g., "work," "idea") or assigned to categories (e.g., "home," "project"). Tasks can be marked as done and given a priority (e.g., "high," "low") and a due date. The application supports keyword searching, grouping by tags or categories, sorting by due date or priority, and generating reports on upcoming deadlines (e.g., "for the next week") and summaries of completed and overdue tasks.

## Functionalities
Below are all the functionalities of our project:
- Creating, editing, deleting and organizing notes and tasks;
- Prioritizing tasks to be done;
- Setting a deadline for tasks;
- Assigning tags and categories;
- Searching and sorting tasks and notes;
- Grouping notes and tasks;
- Generating reports and summaries in .txt, .docx, .pdf formats;

## Design patterns used
- Singleton
- Builder - for creating tasks and notes
- Command - for handling operations on tasks and notes
- Composite - for creating a hierarchy of categories and tags
- Template method - for generating reports and summaries
- MVC/MVVM

## Tech stack
- C#
- WPF
- Visual Studio 2022
  
## Requirements
- .Net 6+
- Visual Studio 2022

## How to use
To run this project, clone this repository:
```bash
git clone https://github.com/Ing862/SuperZTP-projekt
```

# Super Mario Bros. Level 1-1 Recreation

Project Dates: January 2023 - May 2023

## Authors

  - Mark Howard 
  - Quinton Holmes 
  - Brennan Tibbetts
  - Alexis Martinez
  - Chang Lu

## Project Overview

  This project is a recreation of the original Super Mario Bros. Level 1-1 using the MonoGame framework in C#. It replicates key elements such as Mario’s movement and power-ups, enemy AI (Goombas and Koopas), environment interactions, and the classic secret area. 
  Developed as part of a five-person Scrum team, the project emphasizes clean code, design patterns, and collaborative development to simulate the core mechanics of the classic game. 

## Design Patterns

This project incorporates several design patterns to ensure a flexible and maintainable codebase:

  - Observer Pattern: Used to notify different game objects when certain game events occur (e.g., when Mario collects a coin).
  - Object Pooling Pattern: Helps with performance by reusing enemy objects instead of creating/destroying them frequently.
  - Command Pattern: Used for handling user input (e.g., Mario's jump or run commands).
  - Singleton Pattern: Ensures there's only one instance of game-wide managers (e.g., input manager, level manager).
  - Prototype Pattern: Allows for cloning enemies and other game objects to reuse components without duplicating code.
  - Factory Method Pattern: Used for creating enemy objects through different sub-classes.

## Technologies

  - MonoGame Framework: A low-level framework for creating 2D games in C#.
  - C#: The programming language used to develop the game.
  - Visual Studio: The primary development environment for coding and debugging.
  - Azure DevOps: Used for Scrum-based project management with 2-week sprints, task tracking, and code reviews.

## Setup

  Clone this repository
  
  Install MonoGame if you don’t have it:
      Visit MonoGame's official site for installation instructions.
  Open the project in Visual Studio.
  Build and run the project. Mario's first level should load automatically.

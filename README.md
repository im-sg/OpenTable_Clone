
# OpenTable Clone

A simplified clone of OpenTable built with ASP.NET Core MVC and SQLite. This project demonstrates a restaurant listing and reservation system with an admin area for managing restaurants and reservations.

---

## Table of contents

- [Features](#features)  
- [Tech stack](#tech-stack)  
- [Getting started](#getting-started)  
- [Configuration](#configuration)  
- [Database](#database)  
- [Project structure](#project-structure)  
- [Running locally](#running-locally)  
- [Tests](#tests)  
- [Contributing](#contributing)  
- [License](#license)

---

## Features

- Restaurant listing and detail pages  
- Reservation creation (date/time, party size)  
- Admin area for managing restaurants and reservations  
- Persistent storage using SQLite (included DB file)  
- Razor Views (server-side rendered UI)  
- Basic validation and server-side checks  
- Seed/sample data included in SQLite DB (if present)

---

## Tech stack

- .NET (ASP.NET Core MVC)  
- C#  
- Razor views (HTML/CSS)  
- SQLite (file `OpenTableAppDatabase.sqlite` included in repo)  
- Optional: client-side JS for progressive UI improvements

---

## Getting started

### Prerequisites

- [.NET 7 SDK (or .NET 6)](https://dotnet.microsoft.com/en-us/download) — use the version referenced in `OpenTable.csproj` if different.  
- A command-line terminal (Windows `PowerShell` / macOS / Linux).  
- Optional: Visual Studio / VS Code for development and debugging.

### Clone

```bash
git clone https://github.com/im-sg/OpenTable_Clone.git
cd OpenTable_Clone
```

### Restore & run

```bash
dotnet restore
dotnet run
```

By default the app will run on `https://localhost:5001` or `http://localhost:5000`. Check the console output after `dotnet run` for the exact URL.

---

## Configuration

Check `appsettings.json` for configuration options (logging, connection strings, etc.). If the repository includes an SQLite file (`OpenTableAppDatabase.sqlite`) the project likely uses a relative connection string like:

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=OpenTableAppDatabase.sqlite"
}
```

If you want to use your own database, update `appsettings.json` or the DB context registration in `Program.cs`.

---

## Database

- This project ships (based on the repo) with a file `OpenTableAppDatabase.sqlite`.  
- If migrations are used, run (if EF Core is configured):

```bash
dotnet ef database update
```

(Install the `dotnet-ef` tool and EF Core packages if needed.)

If the project seeds data on first run, the seed routine will populate restaurants and sample reservations automatically.

---

## Project structure (high level)

```
/Areas/Admin/        - Admin controllers and views
/Controllers/        - Main MVC controllers (Home, Restaurants, Reservations, etc.)
/Models/             - Domain models (Restaurant, Reservation, User, etc.)
/Views/              - Razor views for the public-facing site
/wwwroot/            - Static files (css, js, images)
Program.cs           - App startup (DI, middlewares, routing)
appsettings.json     - App configuration & connection strings
OpenTableAppDatabase.sqlite - SQLite DB (if included)
```

---

## Common tasks

### Add a new restaurant (developer)

1. Add a new model property in `Models/Restaurant.cs` (if needed).  
2. Update the EF Core DbContext and create a migration:  
   ```bash
   dotnet ef migrations add AddNewFieldToRestaurant
   dotnet ef database update
   ```
3. Update the admin view to show/edit the new field.

### Run in development with hot reload

```bash
dotnet watch run
```

### Debug in Visual Studio / VS Code

- Open the `.sln` file (`OpenTable.sln`) in Visual Studio or the folder in VS Code and use the debugger.

---

## Tests

If there are no test projects included, consider adding an xUnit/NUnit project for:

- Unit tests for services and controllers  
- Integration tests for key flows (reservation CRUD)

---

## Contributing

1. Fork the repo  
2. Create a feature branch: `git checkout -b feat/awesome`  
3. Make your changes and commit: `git commit -m "Add X"`  
4. Push and open a Pull Request

Please include descriptive commit messages and migration files if you modify the DB schema.

---

## Troubleshooting

- **Port in use**: the app will fail to start. Kill the process using that port or change the port in `launchSettings.json` (or via command-line environment variables).  
- **SQLite locked**: ensure no other process (e.g., DB browser) has a lock on the file. Close other tools, or copy the DB to a new file for testing.

---

## License

Add your license file (e.g., `LICENSE` — MIT recommended if you want permissive open-source licensing).

---

## Contact / Maintainer

**Author:** [im-sg](https://github.com/im-sg)  
**Project link:** [https://github.com/im-sg/OpenTable_Clone](https://github.com/im-sg/OpenTable_Clone)

---

### Short Description (for GitHub "About" section)

> OpenTable clone — an ASP.NET Core MVC restaurant listing and reservation system backed by SQLite. Includes admin area and sample database.

---

### Longer Project Summary (for portfolio / README top)

OpenTable Clone is a small, educational ASP.NET Core MVC application that mimics the core features of a reservation platform. Built with C#, Razor views, and SQLite, the project demonstrates routing and controller patterns, CRUD operations for restaurants and reservations, and a lightweight admin area for content management. It’s useful as a learning project for developers who want to explore building server-rendered web apps with .NET and practice working with EF Core and SQLite.

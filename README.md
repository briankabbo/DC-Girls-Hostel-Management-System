# DC Girls Hostel Management System

A Windows desktop application for managing **Dhaka Credit Girls Hostel** operations: user accounts, customers, room bookings, and dashboard summaries. Built with **C# WinForms** on **.NET Framework 4.7.2** and **SQL Server LocalDB**.

---

## Features

| Area | Description |
|------|-------------|
| **Login** | Secure sign-in with hashed passwords (PBKDF2); legacy plain-text passwords are upgraded on first login |
| **Dashboard** | Overview of hostel activity |
| **Customers** | Add, edit, and manage resident records |
| **Bookings** | Room booking management |
| **Users** | Admin user accounts (name, phone, password) |

---

## Tech stack

- **UI:** Windows Forms, Bunifu UI, Guna UI2  
- **Data:** ADO.NET (`System.Data.SqlClient`), repository pattern  
- **Database:** SQL Server LocalDB — instance `(localdb)\DCGirlsHostel`, database `DCGirlsHostelDB`  
- **Security:** Parameterized SQL, `PasswordHasher` (PBKDF2), connection string in `App.config`

---

## Project structure

```
DC-Girls-Hostel-Management-System/
│
├── GMS Kabbo.sln              # Visual Studio solution
├── GHMS Kabbo.csproj          # Main project file
├── Program.cs                 # Entry point; sets data directory and starts login form
├── App.config                 # Connection string (HostelDb) and runtime settings
├── packages.config            # NuGet packages (Guna.UI2.WinForms)
│
├── Data/                      # Data access and security (no UI)
│   ├── DatabaseHelper.cs      # Connection, Execute, Scalar, FillTable
│   ├── LocalDbBootstrap.cs    # Ensures LocalDB instance is running
│   ├── PasswordHasher.cs      # Hash and verify passwords
│   ├── UserRepository.cs      # User CRUD and login validation
│   ├── CustomerRepository.cs  # Customer CRUD
│   ├── BookingRepository.cs   # Booking queries
│   └── RoomRepository.cs      # Room status and pricing
│
├── Forms/                     # WinForms UI
│   ├── NavigationHelper.cs    # Shared navigation between forms
│   ├── login.cs               # Login screen (+ .Designer.cs, .resx)
│   ├── Dashboard.cs           # Main dashboard after login
│   ├── Customers.cs           # Customer management
│   ├── Bookings.cs            # Booking management
│   └── Users.cs               # User administration
│
├── Properties/
│   └── AssemblyInfo.cs        # Assembly metadata
│
├── SQL/
│   ├── CreateDatabase.sql     # Creates database, tables, and seed data
│   └── EnsureLocalDbInstance.bat  # Creates/starts (localdb)\DCGirlsHostel
│
└── libs/
    └── README.txt             # Instructions for Bunifu DLL (not in repo)
```

### Generated / not in source control

These are created locally and listed in `.gitignore`:

| Path | Purpose |
|------|---------|
| `bin/`, `obj/` | Build output |
| `.vs/` | Visual Studio cache |
| `packages/` | NuGet restore (Guna.UI2) |
| `libs/*.dll` | Bunifu UI (you must add the DLL manually) |

---

## Prerequisites

- Windows 10 or later  
- [Visual Studio 2019+](https://visualstudio.microsoft.com/) with **.NET desktop development** workload  
- [SQL Server Express LocalDB](https://learn.microsoft.com/sql/database-engine/configure-windows/sql-server-express-localdb)  
- [SQL Server Management Studio](https://learn.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms) (recommended for running scripts)

---

## Getting started

### 1. Clone the repository

```bash
git clone https://github.com/briankabbo/DC-Girls-Hostel-Management-System.git
cd DC-Girls-Hostel-Management-System
```

### 2. LocalDB instance

From the project root, run (once):

```bat
SQL\EnsureLocalDbInstance.bat
```

This creates and starts `(localdb)\DCGirlsHostel`.

### 3. Create the database

1. Open SSMS and connect to: `(localdb)\DCGirlsHostel`  
2. Open and execute: `SQL\CreateDatabase.sql`

### 4. Dependencies

```bash
nuget restore "GMS Kabbo.sln"
```

Copy **Bunifu.UI.WinForms.1.5.3.dll** into `libs\` (see `libs\README.txt`). Guna UI2 is restored via NuGet.

### 5. Build and run

1. Open `GMS Kabbo.sln` in Visual Studio  
2. **Build** → **Rebuild Solution**  
3. **Debug** → **Start** (or F5)

**Default login:** `admin` / `admin` — change the password after first sign-in.

---

## Configuration

Connection settings are in `App.config` under `connectionStrings` / `HostelDb`:

```xml
Data Source=(localdb)\DCGirlsHostel;Initial Catalog=DCGirlsHostelDB;Integrated Security=True;Connect Timeout=30
```

Edit this only if you use a different server or database name.

---

## Architecture notes

- **Forms** handle UI events only; database logic lives in **`Data/*Repository.cs`**.  
- **`DatabaseHelper`** reads the connection string from config and runs parameterized commands.  
- **`Program.cs`** sets the app `DataDirectory` and ensures LocalDB is running before the login form opens.

---

## Troubleshooting

| Issue | What to try |
|-------|-------------|
| Login / database errors | Run `EnsureLocalDbInstance.bat`, then `CreateDatabase.sql` in SSMS |
| Missing Bunifu reference | Place `Bunifu.UI.WinForms.1.5.3.dll` in `libs\` |
| Missing Guna reference | Run `nuget restore` from the solution folder |
| `(LocalDB)\MSSQLLocalDB` fails | This project uses `(localdb)\DCGirlsHostel` instead (see `App.config`) |

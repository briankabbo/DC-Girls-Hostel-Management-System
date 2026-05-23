# DC Girls Hostel Management System

WinForms desktop app (.NET Framework 4.7.2) for hostel bookings, customers, rooms, and users.

## Project structure

```
├── App.config              # Database connection (HostelDb)
├── Program.cs              # Application entry point
├── Data/                   # Data access and security
│   ├── DatabaseHelper.cs
│   ├── LocalDbBootstrap.cs
│   ├── PasswordHasher.cs
│   ├── UserRepository.cs
│   ├── CustomerRepository.cs
│   ├── BookingRepository.cs
│   └── RoomRepository.cs
├── Forms/                  # UI (logic + designer)
│   ├── NavigationHelper.cs
│   ├── login.cs
│   ├── Dashboard.cs
│   ├── Customers.cs
│   ├── Bookings.cs
│   └── Users.cs
├── Properties/             # Assembly info
├── SQL/
│   ├── CreateDatabase.sql
│   └── EnsureLocalDbInstance.bat
└── libs/                   # Bunifu DLL (see libs/README.txt)
```

## Setup

1. **LocalDB** — Run `SQL\EnsureLocalDbInstance.bat` (creates `(localdb)\DCGirlsHostel`).
2. **Database** — In SSMS, connect to `(localdb)\DCGirlsHostel`, run `SQL\CreateDatabase.sql`.
3. **Dependencies** — `nuget restore`, then copy `Bunifu.UI.WinForms.1.5.3.dll` into `libs\`.
4. **Build** — Open `GMS Kabbo.sln` in Visual Studio and rebuild.

Default login: **admin** / **admin** (change after first login).

## Connection string

Edit `HostelDb` in `App.config`. Default:

`Data Source=(localdb)\DCGirlsHostel;Initial Catalog=DCGirlsHostelDB;Integrated Security=True`

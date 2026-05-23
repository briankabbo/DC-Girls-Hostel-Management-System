@echo off
REM One-time setup: creates a working LocalDB instance for this project (if MSSQLLocalDB fails).
sqllocaldb create DCGirlsHostel 2>nul
sqllocaldb start DCGirlsHostel
echo LocalDB instance (localdb)\DCGirlsHostel is ready.
echo Run CreateDatabase.sql in SSMS connected to (localdb)\DCGirlsHostel

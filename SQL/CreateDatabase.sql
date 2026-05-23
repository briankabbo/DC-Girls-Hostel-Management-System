-- DC Girls Hostel Management System — database setup
-- Run in SSMS against your SQL Server instance (includes LocalDB).

USE master;
GO

IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = N'DCGirlsHostelDB')
BEGIN
    CREATE DATABASE DCGirlsHostelDB;
END
GO

USE DCGirlsHostelDB;
GO

IF OBJECT_ID(N'dbo.UserTbl', N'U') IS NOT NULL DROP TABLE dbo.UserTbl;
IF OBJECT_ID(N'dbo.BookingTbl', N'U') IS NOT NULL DROP TABLE dbo.BookingTbl;
IF OBJECT_ID(N'dbo.CustomerTbl', N'U') IS NOT NULL DROP TABLE dbo.CustomerTbl;
IF OBJECT_ID(N'dbo.RoomTbl', N'U') IS NOT NULL DROP TABLE dbo.RoomTbl;
GO

CREATE TABLE dbo.UserTbl (
    UId    INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Uname  NVARCHAR(100)     NOT NULL,
    Uphone NVARCHAR(50)      NOT NULL,
    Upass  NVARCHAR(100)     NOT NULL
);

CREATE TABLE dbo.CustomerTbl (
    CusId   INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    CusName NVARCHAR(100)     NOT NULL,
    CusPhone NVARCHAR(50)     NOT NULL,
    CusMs   NVARCHAR(20)      NOT NULL,
    CusDOB  DATE              NOT NULL,
    CusRoom NVARCHAR(20)      NOT NULL,
    CusProf NVARCHAR(50)      NOT NULL
);

CREATE TABLE dbo.RoomTbl (
    RId     INT           NOT NULL PRIMARY KEY,
    RType   NVARCHAR(50)  NOT NULL,
    RCost   INT           NOT NULL,
    RStatus NVARCHAR(20)  NOT NULL CONSTRAINT DF_RoomTbl_RStatus DEFAULT (N'Available')
);

CREATE TABLE dbo.BookingTbl (
    BId     INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    CusId   INT               NOT NULL,
    CusName NVARCHAR(100)     NOT NULL,
    RId     INT               NOT NULL,
    RNum    INT               NOT NULL,
    RType   NVARCHAR(50)      NOT NULL,
    BCost   INT               NOT NULL
);
GO

-- 20 rooms (RId 1–20); adjust types/costs as needed
DECLARE @i INT = 1;
WHILE @i <= 20
BEGIN
    INSERT INTO dbo.RoomTbl (RId, RType, RCost, RStatus)
    VALUES (
        @i,
        CASE WHEN @i <= 10 THEN N'Standard' ELSE N'Deluxe' END,
        CASE WHEN @i <= 10 THEN 5000 ELSE 8000 END,
        N'Available'
    );
    SET @i += 1;
END
GO

-- Default admin (change password after first login)
IF NOT EXISTS (SELECT 1 FROM dbo.UserTbl WHERE Uname = N'admin')
BEGIN
    INSERT INTO dbo.UserTbl (Uname, Uphone, Upass)
    VALUES (N'admin', N'0000000000', N'admin');
END
GO

PRINT 'DCGirlsHostelDB is ready.';
GO

-- =============================================
-- Bus Ticket Management System - Database Script
-- SQL Server Database Setup
-- =============================================

-- Create Database
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'BusTicketManagementDB')
BEGIN
    CREATE DATABASE BusTicketManagementDB;
END
GO

USE BusTicketManagementDB;
GO

-- =============================================
-- 1. ASP.NET Core Identity Tables (Extended)
-- =============================================

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AspNetUsers')
BEGIN
    CREATE TABLE AspNetUsers (
        Id NVARCHAR(450) PRIMARY KEY,
        UserName NVARCHAR(256) NOT NULL UNIQUE,
        Email NVARCHAR(256) UNIQUE,
        PasswordHash NVARCHAR(MAX),
        PhoneNumber NVARCHAR(20),
        FirstName NVARCHAR(100),
        LastName NVARCHAR(100),
        DateOfBirth DATE,
        IsActive BIT DEFAULT 1,
        CreatedAt DATETIME DEFAULT GETUTCDATE(),
        UpdatedAt DATETIME DEFAULT GETUTCDATE()
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AspNetRoles')
BEGIN
    CREATE TABLE AspNetRoles (
        Id NVARCHAR(450) PRIMARY KEY,
        Name NVARCHAR(256) NOT NULL UNIQUE,
        Description NVARCHAR(500),
        IsActive BIT DEFAULT 1
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AspNetUserRoles')
BEGIN
    CREATE TABLE AspNetUserRoles (
        UserId NVARCHAR(450),
        RoleId NVARCHAR(450),
        PRIMARY KEY (UserId, RoleId),
        FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id),
        FOREIGN KEY (RoleId) REFERENCES AspNetRoles(Id)
    );
END
GO

-- =============================================
-- 2. Bus Management Tables
-- =============================================

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Buses')
BEGIN
    CREATE TABLE Buses (
        BusId INT PRIMARY KEY IDENTITY(1,1),
        BusNo NVARCHAR(50) NOT NULL UNIQUE,
        BusTypeId INT,
        TotalCapacity INT NOT NULL,
        CurrentOccupancy INT DEFAULT 0,
        IsActive BIT DEFAULT 1,
        CreatedAt DATETIME DEFAULT GETUTCDATE(),
        UpdatedAt DATETIME DEFAULT GETUTCDATE()
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Routes')
BEGIN
    CREATE TABLE Routes (
        RouteId INT PRIMARY KEY IDENTITY(1,1),
        RouteName NVARCHAR(200) NOT NULL,
        SourceCity NVARCHAR(100) NOT NULL,
        DestinationCity NVARCHAR(100) NOT NULL,
        TotalDistance DECIMAL(10,2),
        BaseFare DECIMAL(10,2),
        IsActive BIT DEFAULT 1,
        CreatedAt DATETIME DEFAULT GETUTCDATE(),
        UpdatedAt DATETIME DEFAULT GETUTCDATE()
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Schedules')
BEGIN
    CREATE TABLE Schedules (
        ScheduleId INT PRIMARY KEY IDENTITY(1,1),
        BusId INT NOT NULL,
        RouteId INT NOT NULL,
        DepartureTime DATETIME NOT NULL,
        ArrivalTime DATETIME NOT NULL,
        ScheduleDate DATE NOT NULL,
        Status NVARCHAR(50) DEFAULT 'Scheduled',
        IsActive BIT DEFAULT 1,
        CreatedAt DATETIME DEFAULT GETUTCDATE(),
        FOREIGN KEY (BusId) REFERENCES Buses(BusId),
        FOREIGN KEY (RouteId) REFERENCES Routes(RouteId)
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Seats')
BEGIN
    CREATE TABLE Seats (
        SeatId INT PRIMARY KEY IDENTITY(1,1),
        BusId INT NOT NULL,
        SeatNumber NVARCHAR(10) NOT NULL,
        IsActive BIT DEFAULT 1,
        CreatedAt DATETIME DEFAULT GETUTCDATE(),
        FOREIGN KEY (BusId) REFERENCES Buses(BusId)
    );
END
GO

-- =============================================
-- 3. Booking Management Tables
-- =============================================

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Bookings')
BEGIN
    CREATE TABLE Bookings (
        BookingId INT PRIMARY KEY IDENTITY(1,1),
        BookingRef NVARCHAR(50) NOT NULL UNIQUE,
        ScheduleId INT NOT NULL,
        PassengerId NVARCHAR(450) NOT NULL,
        TotalSeats INT NOT NULL,
        TotalAmount DECIMAL(10,2) NOT NULL,
        BookingStatus NVARCHAR(50) DEFAULT 'Pending',
        PaymentStatus NVARCHAR(50) DEFAULT 'Pending',
        BookingDate DATETIME DEFAULT GETUTCDATE(),
        CancellationDate DATETIME,
        IsActive BIT DEFAULT 1,
        CreatedAt DATETIME DEFAULT GETUTCDATE(),
        UpdatedAt DATETIME DEFAULT GETUTCDATE(),
        FOREIGN KEY (ScheduleId) REFERENCES Schedules(ScheduleId),
        FOREIGN KEY (PassengerId) REFERENCES AspNetUsers(Id)
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Payments')
BEGIN
    CREATE TABLE Payments (
        PaymentId INT PRIMARY KEY IDENTITY(1,1),
        BookingId INT NOT NULL,
        Amount DECIMAL(10,2) NOT NULL,
        PaymentStatus NVARCHAR(50) DEFAULT 'Pending',
        PaymentDate DATETIME,
        CreatedAt DATETIME DEFAULT GETUTCDATE(),
        FOREIGN KEY (BookingId) REFERENCES Bookings(BookingId)
    );
END
GO

-- =============================================
-- 4. Chat System Tables
-- =============================================

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ChatSessions')
BEGIN
    CREATE TABLE ChatSessions (
        ChatSessionId INT PRIMARY KEY IDENTITY(1,1),
        UserId NVARCHAR(450) NOT NULL,
        StartTime DATETIME DEFAULT GETUTCDATE(),
        EndTime DATETIME,
        Status NVARCHAR(50) DEFAULT 'Active',
        FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id)
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ChatMessages')
BEGIN
    CREATE TABLE ChatMessages (
        ChatMessageId INT PRIMARY KEY IDENTITY(1,1),
        ChatSessionId INT NOT NULL,
        SenderType NVARCHAR(50),
        Message NVARCHAR(MAX) NOT NULL,
        Timestamp DATETIME DEFAULT GETUTCDATE(),
        FOREIGN KEY (ChatSessionId) REFERENCES ChatSessions(ChatSessionId)
    );
END
GO

PRINT 'Database creation completed successfully!'

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF SCHEMA_ID(N'administration') IS NULL EXEC(N'CREATE SCHEMA [administration];');

GO

CREATE TABLE [OutboxMessages] (
    [Id] uniqueidentifier NOT NULL,
    [OccurredOn] datetime2 NOT NULL,
    [Type] nvarchar(max) NULL,
    [Data] nvarchar(max) NULL,
    [ProcessedDate] datetime2 NULL,
    CONSTRAINT [PK_OutboxMessages] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [administration].[BusinessPartners] (
    [Id] uniqueidentifier NOT NULL,
    [CreateDate] datetime2 NOT NULL,
    [EmailAddress] nvarchar(255) NULL,
    [FirstName] nvarchar(100) NOT NULL,
    [IsActive] bit NOT NULL,
    [LastName] nvarchar(100) NULL,
    [FullName] nvarchar(200) NULL,
    [PhoneNumber] nvarchar(15) NULL,
    [UpdateDate] datetime2 NULL,
    [Street] nvarchar(150) NULL,
    [City] nvarchar(100) NULL,
    [PostalCode] nvarchar(100) NULL,
    [State] nvarchar(100) NULL,
    CONSTRAINT [PK_BusinessPartners] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [administration].[InternalCommands] (
    [Id] uniqueidentifier NOT NULL,
    [Type] nvarchar(max) NULL,
    [Data] nvarchar(max) NULL,
    [ProcessedDate] datetime2 NULL,
    CONSTRAINT [PK_InternalCommands] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [administration].[Staffs] (
    [Id] uniqueidentifier NOT NULL,
    [CreateDate] datetime2 NOT NULL,
    [DateOfBirth] date NULL,
    [EmailAddress] nvarchar(255) NOT NULL,
    [FirstName] nvarchar(100) NOT NULL,
    [IsActive] bit NOT NULL,
    [JobTitle] nvarchar(50) NULL,
    [LastName] nvarchar(100) NOT NULL,
    [MiddleName] nvarchar(100) NULL,
    [FullName] nvarchar(100) NOT NULL,
    [PhoneNumber] nvarchar(15) NULL,
    [StaffNumber] nvarchar(50) NULL,
    [Gender] nvarchar(50) NULL,
    CONSTRAINT [PK_Staffs] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191218162910_AdministrationInitial', N'3.0.0');

GO


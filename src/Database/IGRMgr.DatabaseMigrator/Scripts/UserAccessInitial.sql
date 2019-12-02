IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF SCHEMA_ID(N'users') IS NULL EXEC(N'CREATE SCHEMA [users];');

GO

CREATE TABLE [users].[InternalCommands] (
    [Id] uniqueidentifier NOT NULL,
    [Type] nvarchar(max) NULL,
    [Data] nvarchar(max) NULL,
    [ProcessedDate] datetime2 NULL,
    CONSTRAINT [PK_InternalCommands] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [users].[OutboxMessages] (
    [Id] uniqueidentifier NOT NULL,
    [OccurredOn] datetime2 NOT NULL,
    [Type] nvarchar(max) NULL,
    [Data] nvarchar(max) NULL,
    [ProcessedDate] datetime2 NULL,
    CONSTRAINT [PK_OutboxMessages] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [users].[UserRegistrations] (
    [Id] uniqueidentifier NOT NULL,
    [ConfirmedDate] datetime2 NULL,
    [RegisterDate] datetime2 NOT NULL,
    [Email] nvarchar(max) NULL,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [MiddleName] nvarchar(max) NULL,
    [Name] nvarchar(max) NULL,
    [StatusCode] nvarchar(max) NULL,
    CONSTRAINT [PK_UserRegistrations] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [users].[UserRoles] (
    [RoleCode] nvarchar(450) NOT NULL,
    [UserRegistrationId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY ([UserRegistrationId], [RoleCode]),
    CONSTRAINT [FK_UserRoles_UserRegistrations_UserRegistrationId] FOREIGN KEY ([UserRegistrationId]) REFERENCES [users].[UserRegistrations] ([Id]) ON DELETE CASCADE
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191120092905_UserAccessInitial', N'3.0.0');

GO


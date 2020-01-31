ALTER SCHEMA [administration] TRANSFER [OutboxMessages];

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191219123737_AdministrationInitialOutbox', N'3.0.0');

GO


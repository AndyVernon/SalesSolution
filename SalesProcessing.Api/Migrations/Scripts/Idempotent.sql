IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124183515_InitialCreate'
)
BEGIN
    IF OBJECT_ID(N'[dbo].[DailySales]', N'U') IS NULL
    BEGIN
        CREATE TABLE [dbo].[DailySales] (
            [Id] uniqueidentifier NOT NULL,
            [CustomerId] uniqueidentifier NOT NULL,
            [BranchId] uniqueidentifier NOT NULL,
            [Date] datetime2 NOT NULL,
            [Amount] decimal(18,2) NOT NULL,
            [UploadedDate] datetime2 NOT NULL,
            CONSTRAINT [PK_DailySales] PRIMARY KEY ([Id])
        );
    END;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124183515_InitialCreate'
)
BEGIN
    IF OBJECT_ID(N'[dbo].[WeeklySales]', N'U') IS NULL
    BEGIN
        CREATE TABLE [dbo].[WeeklySales] (
            [Id] uniqueidentifier NOT NULL,
            [YearNumber] int NOT NULL,
            [WeekNumber] int NOT NULL,
            [CustomerId] uniqueidentifier NOT NULL,
            [BranchId] uniqueidentifier NOT NULL,
            [Amount] decimal(18,2) NOT NULL,
            CONSTRAINT [PK_WeeklySales] PRIMARY KEY ([Id])
        );
    END;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124183515_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261124183515_InitialCreate', N'10.0.15');
END;

COMMIT;
GO


using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesProcessing.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
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
                """);

            migrationBuilder.Sql("""
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
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailySales");

            migrationBuilder.DropTable(
                name: "WeeklySales");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Hexed_Relics.Migrations
{
    public partial class Fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternalVariables");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "TotalBalance",
                table: "Users",
                type: "real",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<float>(type: "real", nullable: true),
                    AccNumber = table.Column<int>(type: "integer", nullable: true),
                    DtTst = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeposit = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TotalBalance",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "InternalVariables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccNumber = table.Column<int>(type: "integer", nullable: true),
                    CurrentBalance = table.Column<float>(type: "real", nullable: true),
                    HasBalance = table.Column<bool>(type: "boolean", nullable: true),
                    IsEligibleForPremium = table.Column<bool>(type: "boolean", nullable: true),
                    MoneyIn = table.Column<float>(type: "real", nullable: true),
                    MoneyOut = table.Column<float>(type: "real", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    TotalBalance = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalVariables", x => x.Id);
                });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hexed_Relics.Migrations
{
    public partial class Third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TwoFactorAuthEnabled",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorAuthEnabled",
                table: "Users",
                type: "boolean",
                nullable: true);
        }
    }
}

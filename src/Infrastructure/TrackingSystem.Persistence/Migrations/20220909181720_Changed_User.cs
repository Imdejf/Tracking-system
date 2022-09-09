using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JustCommerce.Persistence.Migrations
{
    public partial class Changed_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "User",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "User",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "ProfileType",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Theme",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ProfileType",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Theme",
                table: "User");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JustCommerce.Persistence.Migrations
{
    public partial class AddedDriverRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Truck",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Truck_UserId",
                table: "Truck",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Truck_User_UserId",
                table: "Truck",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Truck_User_UserId",
                table: "Truck");

            migrationBuilder.DropIndex(
                name: "IX_Truck_UserId",
                table: "Truck");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Truck");
        }
    }
}

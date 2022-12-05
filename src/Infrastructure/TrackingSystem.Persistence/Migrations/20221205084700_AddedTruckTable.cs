using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JustCommerce.Persistence.Migrations
{
    public partial class AddedTruckTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Truck",
                columns: table => new
                {
                    TruckId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RegisterNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Truck", x => x.TruckId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TruckDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TruckId = table.Column<int>(type: "int", nullable: false),
                    LastLatitude = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    LastLongitude = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    LastLocalizationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IgnitionState = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Speed = table.Column<int>(type: "int", nullable: false),
                    Heading = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruckDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TruckDetails_Truck_TruckId",
                        column: x => x.TruckId,
                        principalTable: "Truck",
                        principalColumn: "TruckId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserTruck",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TruckId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTruck", x => new { x.UserId, x.TruckId });
                    table.ForeignKey(
                        name: "FK_UserTruck_Truck_TruckId",
                        column: x => x.TruckId,
                        principalTable: "Truck",
                        principalColumn: "TruckId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTruck_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Truck_TruckId",
                table: "Truck",
                column: "TruckId");

            migrationBuilder.CreateIndex(
                name: "IX_TruckDetails_Id",
                table: "TruckDetails",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TruckDetails_TruckId",
                table: "TruckDetails",
                column: "TruckId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserTruck_TruckId",
                table: "UserTruck",
                column: "TruckId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TruckDetails");

            migrationBuilder.DropTable(
                name: "UserTruck");

            migrationBuilder.DropTable(
                name: "Truck");
        }
    }
}

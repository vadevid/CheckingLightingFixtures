using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainApp.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Login = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lamp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Glows = table.Column<bool>(type: "INTEGER", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RoomId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lamp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lamp_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Login", "Password" },
                values: new object[] { 1, "123", "123" });

            migrationBuilder.InsertData(
                table: "Room",
                column: "Id",
                value: 1);

            migrationBuilder.InsertData(
                table: "Room",
                column: "Id",
                value: 2);

            migrationBuilder.InsertData(
                table: "Lamp",
                columns: new[] { "Id", "Glows", "RoomId", "TimeStamp" },
                values: new object[] { 1, true, 1, new DateTime(2022, 12, 11, 22, 20, 54, 379, DateTimeKind.Local).AddTicks(6453) });

            migrationBuilder.InsertData(
                table: "Lamp",
                columns: new[] { "Id", "Glows", "RoomId", "TimeStamp" },
                values: new object[] { 2, true, 1, new DateTime(2022, 12, 11, 22, 20, 54, 379, DateTimeKind.Local).AddTicks(6468) });

            migrationBuilder.InsertData(
                table: "Lamp",
                columns: new[] { "Id", "Glows", "RoomId", "TimeStamp" },
                values: new object[] { 3, true, 2, new DateTime(2022, 12, 11, 22, 20, 54, 379, DateTimeKind.Local).AddTicks(6470) });

            migrationBuilder.InsertData(
                table: "Lamp",
                columns: new[] { "Id", "Glows", "RoomId", "TimeStamp" },
                values: new object[] { 4, false, 2, new DateTime(2022, 12, 11, 22, 20, 54, 379, DateTimeKind.Local).AddTicks(6471) });

            migrationBuilder.CreateIndex(
                name: "IX_Lamp_RoomId",
                table: "Lamp",
                column: "RoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Lamp");

            migrationBuilder.DropTable(
                name: "Room");
        }
    }
}

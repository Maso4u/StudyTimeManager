using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudyTimeManager.WPF.UI.Migrations
{
    public partial class CreatingUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Semesters",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_UserId",
                table: "Semesters",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_Users_UserId",
                table: "Semesters",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_Users_UserId",
                table: "Semesters");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Semesters_UserId",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Semesters");
        }
    }
}

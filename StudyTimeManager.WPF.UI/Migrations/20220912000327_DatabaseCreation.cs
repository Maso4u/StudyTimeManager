using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace StudyTimeManager.WPF.UI.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    SemesterId = table.Column<Guid>(type: "TEXT", nullable: false),
                    NumberOfWeeks = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.SemesterId);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    ModuleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    NumberOfCredits = table.Column<int>(type: "INTEGER", nullable: false),
                    ClassHoursPerWeek = table.Column<int>(type: "INTEGER", nullable: false),
                    RequiredWeeklySelfStudyHours = table.Column<int>(type: "INTEGER", nullable: false),
                    SemesterId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.ModuleId);
                    table.ForeignKey(
                        name: "FK_Modules_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "SemesterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModuleSemesterWeeks",
                columns: table => new
                {
                    ModuleSemesterWeekId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    WeekNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    RemainingSelfStudyHours = table.Column<int>(type: "INTEGER", nullable: false),
                    ModuleId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleSemesterWeeks", x => x.ModuleSemesterWeekId);
                    table.ForeignKey(
                        name: "FK_ModuleSemesterWeeks_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudySessions",
                columns: table => new
                {
                    StudySessionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HoursSpent = table.Column<int>(type: "INTEGER", nullable: false),
                    ModuleSemesterWeekId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudySessions", x => x.StudySessionId);
                    table.ForeignKey(
                        name: "FK_StudySessions_ModuleSemesterWeeks_ModuleSemesterWeekId",
                        column: x => x.ModuleSemesterWeekId,
                        principalTable: "ModuleSemesterWeeks",
                        principalColumn: "ModuleSemesterWeekId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Modules_SemesterId",
                table: "Modules",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleSemesterWeeks_ModuleId",
                table: "ModuleSemesterWeeks",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_StudySessions_ModuleSemesterWeekId",
                table: "StudySessions",
                column: "ModuleSemesterWeekId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudySessions");

            migrationBuilder.DropTable(
                name: "ModuleSemesterWeeks");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "Semesters");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Timekeeping.Repositories.Migrations
{
    public partial class Bootstrapper : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    project_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    project_name = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    project_job_journey_charge = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_projects_project_id", x => x.project_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    project_id = table.Column<int>(type: "INTEGER", nullable: true),
                    user_name = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    user_email = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users_user_id", x => x.user_id);
                    table.ForeignKey(
                        name: "fk_projects_users_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "time_entries",
                columns: table => new
                {
                    time_entry_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    project_id = table.Column<int>(type: "INTEGER", nullable: false),
                    user_id = table.Column<int>(type: "INTEGER", nullable: false),
                    time_entry_date = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    time_entry_amount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_time_entries_time_entry_id", x => x.time_entry_id);
                    table.ForeignKey(
                        name: "fk_projects_time_entries_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_users_time_entries_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_projects_project_name",
                table: "projects",
                column: "project_name");

            migrationBuilder.CreateIndex(
                name: "ix_time_entries_time_entry_date",
                table: "time_entries",
                column: "time_entry_date");

            migrationBuilder.CreateIndex(
                name: "IX_time_entries_project_id",
                table: "time_entries",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_time_entries_user_id",
                table: "time_entries",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_user_email",
                table: "users",
                column: "user_email");

            migrationBuilder.CreateIndex(
                name: "IX_users_project_id",
                table: "users",
                column: "project_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "time_entries");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "projects");
        }
    }
}

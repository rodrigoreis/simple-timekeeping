using Microsoft.EntityFrameworkCore.Migrations;

namespace Timekeeping.Repositories.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    user_name = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    user_email = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users_user_id", x => x.user_id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_users_user_email",
                table: "users",
                column: "user_email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace MyNotebook.Data.Migrations
{
    public partial class changedReminderAttr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateadAt",
                table: "Reminder",
                newName: "CreatedAt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Reminder",
                newName: "CreateadAt");
        }
    }
}

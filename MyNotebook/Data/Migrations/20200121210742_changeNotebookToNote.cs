using Microsoft.EntityFrameworkCore.Migrations;

namespace MyNotebook.Data.Migrations
{
    public partial class changeNotebookToNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NotebookId",
                table: "Note",
                newName: "NoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NoteId",
                table: "Note",
                newName: "NotebookId");
        }
    }
}

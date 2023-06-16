using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class createCollaboratorTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Collaborators",
                columns: table => new
                {
                    CollaborateID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollaborateEmail = table.Column<string>(nullable: true),
                    UserID = table.Column<int>(nullable: false),
                    NoteID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collaborators", x => x.CollaborateID);
                    table.ForeignKey(
                        name: "FK_Collaborators_FundooNotes_NoteID",
                        column: x => x.NoteID,
                        principalTable: "FundooNotes",
                        principalColumn: "NoteID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Collaborators_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_NoteID",
                table: "Collaborators",
                column: "NoteID");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_UserID",
                table: "Collaborators",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Collaborators");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerSession.Migrations
{
    public partial class AddingInvitationJoinTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invitation",
                columns: table => new
                {
                    TastingID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitation", x => new { x.TastingID, x.UserID });
                    table.ForeignKey(
                        name: "FK_Invitation_Tasting_TastingID",
                        column: x => x.TastingID,
                        principalTable: "Tasting",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invitation_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_UserID",
                table: "Invitation",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invitation");
        }
    }
}

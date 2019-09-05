using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerSession.Migrations
{
    public partial class CapitalLetters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTasting_Tasting_TastingId",
                table: "UserTasting");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTasting_User_UserId",
                table: "UserTasting");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserTasting",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "TastingId",
                table: "UserTasting",
                newName: "TastingID");

            migrationBuilder.RenameIndex(
                name: "IX_UserTasting_UserId",
                table: "UserTasting",
                newName: "IX_UserTasting_UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTasting_Tasting_TastingID",
                table: "UserTasting",
                column: "TastingID",
                principalTable: "Tasting",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTasting_User_UserID",
                table: "UserTasting",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTasting_Tasting_TastingID",
                table: "UserTasting");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTasting_User_UserID",
                table: "UserTasting");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "UserTasting",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "TastingID",
                table: "UserTasting",
                newName: "TastingId");

            migrationBuilder.RenameIndex(
                name: "IX_UserTasting_UserID",
                table: "UserTasting",
                newName: "IX_UserTasting_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTasting_Tasting_TastingId",
                table: "UserTasting",
                column: "TastingId",
                principalTable: "Tasting",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTasting_User_UserId",
                table: "UserTasting",
                column: "UserId",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

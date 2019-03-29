using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerSession.Data.Migrations
{
    public partial class SessionMeister : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SessionMeisterID",
                table: "Tasting",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasting_SessionMeisterID",
                table: "Tasting",
                column: "SessionMeisterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasting_User_SessionMeisterID",
                table: "Tasting",
                column: "SessionMeisterID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasting_User_SessionMeisterID",
                table: "Tasting");

            migrationBuilder.DropIndex(
                name: "IX_Tasting_SessionMeisterID",
                table: "Tasting");

            migrationBuilder.DropColumn(
                name: "SessionMeisterID",
                table: "Tasting");
        }
    }
}

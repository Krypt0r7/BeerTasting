using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerSession.Migrations
{
    public partial class ListOfMeisters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "User",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_UserID",
                table: "User",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_User_User_UserID",
                table: "User",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_User_UserID",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_UserID",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "User");
        }
    }
}

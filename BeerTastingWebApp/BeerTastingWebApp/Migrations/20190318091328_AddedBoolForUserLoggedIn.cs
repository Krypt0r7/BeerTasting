using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerTastingWebApp.Migrations
{
    public partial class AddedBoolForUserLoggedIn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "LoggedIn",
                table: "User",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoggedIn",
                table: "User");
        }
    }
}

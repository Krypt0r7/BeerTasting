using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerSession.Data.Migrations
{
    public partial class UpdatedJoinTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserTasting");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserTasting",
                nullable: false,
                defaultValue: 0);
        }
    }
}

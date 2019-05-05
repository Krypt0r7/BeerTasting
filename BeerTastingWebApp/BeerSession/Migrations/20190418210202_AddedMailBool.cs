using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerSession.Migrations
{
    public partial class AddedMailBool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MailSent",
                table: "Participant",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MailSent",
                table: "Participant");
        }
    }
}

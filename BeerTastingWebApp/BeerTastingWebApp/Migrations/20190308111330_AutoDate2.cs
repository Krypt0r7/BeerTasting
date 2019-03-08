using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerTastingWebApp.Migrations
{
    public partial class AutoDate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Tasting",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 8, 12, 13, 30, 190, DateTimeKind.Local).AddTicks(4335),
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Tasting",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 8, 12, 13, 30, 190, DateTimeKind.Local).AddTicks(4335));
        }
    }
}

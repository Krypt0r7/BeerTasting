using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerSession.Migrations
{
    public partial class RemovedManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasting_TastingRoom_TastingRoomID",
                table: "Tasting");

            migrationBuilder.DropTable(
                name: "UserTastingRooms");

            migrationBuilder.DropIndex(
                name: "IX_Tasting_TastingRoomID",
                table: "Tasting");

            migrationBuilder.DropColumn(
                name: "TastingRoomID",
                table: "Tasting");

            migrationBuilder.AddColumn<int>(
                name: "TastingId",
                table: "TastingRoom",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TastingRoom_TastingId",
                table: "TastingRoom",
                column: "TastingId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TastingRoom_Tasting_TastingId",
                table: "TastingRoom",
                column: "TastingId",
                principalTable: "Tasting",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TastingRoom_Tasting_TastingId",
                table: "TastingRoom");

            migrationBuilder.DropIndex(
                name: "IX_TastingRoom_TastingId",
                table: "TastingRoom");

            migrationBuilder.DropColumn(
                name: "TastingId",
                table: "TastingRoom");

            migrationBuilder.AddColumn<int>(
                name: "TastingRoomID",
                table: "Tasting",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserTastingRooms",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    TastingRoomId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTastingRooms", x => new { x.UserId, x.TastingRoomId });
                    table.ForeignKey(
                        name: "FK_UserTastingRooms_TastingRoom_TastingRoomId",
                        column: x => x.TastingRoomId,
                        principalTable: "TastingRoom",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTastingRooms_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasting_TastingRoomID",
                table: "Tasting",
                column: "TastingRoomID");

            migrationBuilder.CreateIndex(
                name: "IX_UserTastingRooms_TastingRoomId",
                table: "UserTastingRooms",
                column: "TastingRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasting_TastingRoom_TastingRoomID",
                table: "Tasting",
                column: "TastingRoomID",
                principalTable: "TastingRoom",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

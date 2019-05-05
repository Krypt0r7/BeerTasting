using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerSession.Migrations
{
    public partial class RemovedRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Connection");

            migrationBuilder.DropTable(
                name: "TastingRoom");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Connection",
                columns: table => new
                {
                    ConnectionID = table.Column<string>(nullable: false),
                    Connected = table.Column<bool>(nullable: false),
                    UserAgent = table.Column<string>(nullable: true),
                    UserID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connection", x => x.ConnectionID);
                    table.ForeignKey(
                        name: "FK_Connection_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TastingRoom",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoomName = table.Column<string>(nullable: true),
                    TastingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TastingRoom", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TastingRoom_Tasting_TastingId",
                        column: x => x.TastingId,
                        principalTable: "Tasting",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Connection_UserID",
                table: "Connection",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TastingRoom_TastingId",
                table: "TastingRoom",
                column: "TastingId",
                unique: true);
        }
    }
}

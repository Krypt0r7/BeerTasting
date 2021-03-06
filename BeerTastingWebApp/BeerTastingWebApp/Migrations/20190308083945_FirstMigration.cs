﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerTastingWebApp.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    SignedUp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tasting",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    SessionMeisterID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasting", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tasting_User_SessionMeisterID",
                        column: x => x.SessionMeisterID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Beer",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Producer = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    SystemetNumber = table.Column<int>(nullable: false),
                    TastingID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Beer_Tasting_TastingID",
                        column: x => x.TastingID,
                        principalTable: "Tasting",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserTasting",
                columns: table => new
                {
                    TastingId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTasting", x => new { x.TastingId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserTasting_Tasting_TastingId",
                        column: x => x.TastingId,
                        principalTable: "Tasting",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTasting_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beer_TastingID",
                table: "Beer",
                column: "TastingID");

            migrationBuilder.CreateIndex(
                name: "IX_Tasting_SessionMeisterID",
                table: "Tasting",
                column: "SessionMeisterID");

            migrationBuilder.CreateIndex(
                name: "IX_UserTasting_UserId",
                table: "UserTasting",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Beer");

            migrationBuilder.DropTable(
                name: "UserTasting");

            migrationBuilder.DropTable(
                name: "Tasting");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}

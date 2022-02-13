using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HigherOrLower.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Deck",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Cards = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deck", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HigherOrLowerGames",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    LastCard = table.Column<string>(type: "TEXT", nullable: false),
                    DeckId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HigherOrLowerGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HigherOrLowerGames_Deck_DeckId",
                        column: x => x.DeckId,
                        principalTable: "Deck",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Score = table.Column<int>(type: "INTEGER", nullable: false),
                    HigherOrLowerGameId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Player_HigherOrLowerGames_HigherOrLowerGameId",
                        column: x => x.HigherOrLowerGameId,
                        principalTable: "HigherOrLowerGames",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HigherOrLowerGames_DeckId",
                table: "HigherOrLowerGames",
                column: "DeckId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_HigherOrLowerGameId",
                table: "Player",
                column: "HigherOrLowerGameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "HigherOrLowerGames");

            migrationBuilder.DropTable(
                name: "Deck");
        }
    }
}

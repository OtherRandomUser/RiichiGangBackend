using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RiichiGang.Data.Migrations
{
    public partial class SixthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AllowNonMembers",
                table: "Tournaments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Tournaments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Tournaments",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RequirePermission",
                table: "Tournaments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RulesetId",
                table: "Tournaments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Brackets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    TournamentId = table.Column<int>(nullable: false),
                    Sequence = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    WinCondition = table.Column<int>(nullable: false),
                    NumberOfAdvancing = table.Column<int>(nullable: false),
                    NumberOfSeries = table.Column<int>(nullable: false),
                    GamesPerSeries = table.Column<int>(nullable: false),
                    FinalScoreMultiplier = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brackets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brackets_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TournamentPlayers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    TournamentId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentPlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentPlayers_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TournamentPlayers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BracketPlayers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    PlayerId = table.Column<int>(nullable: false),
                    BracketId = table.Column<int>(nullable: false),
                    Score = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BracketPlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BracketPlayers_Brackets_BracketId",
                        column: x => x.BracketId,
                        principalTable: "Brackets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BracketPlayers_TournamentPlayers_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "TournamentPlayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    BracketId = table.Column<int>(nullable: false),
                    Series = table.Column<int>(nullable: false),
                    TonchaId = table.Column<int>(nullable: false),
                    NanchaId = table.Column<int>(nullable: false),
                    ShachaId = table.Column<int>(nullable: false),
                    PeichaId = table.Column<int>(nullable: false),
                    PlayedAt = table.Column<DateTime>(nullable: true),
                    LogFile = table.Column<string>(nullable: true),
                    MatchResultTon = table.Column<int>(nullable: false),
                    EndScoreTon = table.Column<float>(nullable: false),
                    MatchResultNan = table.Column<int>(nullable: false),
                    EndScoreNan = table.Column<float>(nullable: false),
                    MatchResultSha = table.Column<int>(nullable: false),
                    EndScoreSha = table.Column<float>(nullable: false),
                    MatchResultPei = table.Column<int>(nullable: false),
                    EndScorePei = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Brackets_BracketId",
                        column: x => x.BracketId,
                        principalTable: "Brackets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_BracketPlayers_NanchaId",
                        column: x => x.NanchaId,
                        principalTable: "BracketPlayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_BracketPlayers_PeichaId",
                        column: x => x.PeichaId,
                        principalTable: "BracketPlayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_BracketPlayers_ShachaId",
                        column: x => x.ShachaId,
                        principalTable: "BracketPlayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_BracketPlayers_TonchaId",
                        column: x => x.TonchaId,
                        principalTable: "BracketPlayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_RulesetId",
                table: "Tournaments",
                column: "RulesetId");

            migrationBuilder.CreateIndex(
                name: "IX_BracketPlayers_BracketId",
                table: "BracketPlayers",
                column: "BracketId");

            migrationBuilder.CreateIndex(
                name: "IX_BracketPlayers_PlayerId",
                table: "BracketPlayers",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Brackets_TournamentId",
                table: "Brackets",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_BracketId",
                table: "Games",
                column: "BracketId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_NanchaId",
                table: "Games",
                column: "NanchaId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_PeichaId",
                table: "Games",
                column: "PeichaId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_ShachaId",
                table: "Games",
                column: "ShachaId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_TonchaId",
                table: "Games",
                column: "TonchaId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentPlayers_TournamentId",
                table: "TournamentPlayers",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentPlayers_UserId",
                table: "TournamentPlayers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_Rulesets_RulesetId",
                table: "Tournaments",
                column: "RulesetId",
                principalTable: "Rulesets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_Rulesets_RulesetId",
                table: "Tournaments");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "BracketPlayers");

            migrationBuilder.DropTable(
                name: "Brackets");

            migrationBuilder.DropTable(
                name: "TournamentPlayers");

            migrationBuilder.DropIndex(
                name: "IX_Tournaments_RulesetId",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "AllowNonMembers",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "RequirePermission",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "RulesetId",
                table: "Tournaments");
        }
    }
}

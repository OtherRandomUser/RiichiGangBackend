using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RiichiGang.Data.Migrations
{
    public partial class SeventhMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Brackets_BracketId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_BracketPlayers_NanchaId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_BracketPlayers_PeichaId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_BracketPlayers_ShachaId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_BracketPlayers_TonchaId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_NanchaId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_PeichaId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_ShachaId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_TonchaId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "EndScoreNan",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "EndScorePei",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "EndScoreSha",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "EndScoreTon",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "MatchResultNan",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "MatchResultPei",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "MatchResultSha",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "MatchResultTon",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "NanchaId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "PeichaId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Series",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ShachaId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "TonchaId",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "ClubId",
                table: "Tournaments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Tournaments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Tournaments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "BracketId",
                table: "Games",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "LogLink",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeriesId",
                table: "Games",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Player1_EndScore",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Player1_MatchResult",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Player1_RunningTotal",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Player1_Seat",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Player2_EndScore",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Player2_MatchResult",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Player2_RunningTotal",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Player2_Seat",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Player3_EndScore",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Player3_MatchResult",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Player3_RunningTotal",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Player3_Seat",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Player4_EndScore",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Player4_MatchResult",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Player4_RunningTotal",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Player4_Seat",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Placement",
                table: "BracketPlayers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    BracketId = table.Column<int>(nullable: false),
                    Player1Id = table.Column<int>(nullable: false),
                    Player2Id = table.Column<int>(nullable: false),
                    Player3Id = table.Column<int>(nullable: false),
                    Player4Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Series_Brackets_BracketId",
                        column: x => x.BracketId,
                        principalTable: "Brackets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Series_BracketPlayers_Player1Id",
                        column: x => x.Player1Id,
                        principalTable: "BracketPlayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Series_BracketPlayers_Player2Id",
                        column: x => x.Player2Id,
                        principalTable: "BracketPlayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Series_BracketPlayers_Player3Id",
                        column: x => x.Player3Id,
                        principalTable: "BracketPlayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Series_BracketPlayers_Player4Id",
                        column: x => x.Player4Id,
                        principalTable: "BracketPlayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_ClubId",
                table: "Tournaments",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_SeriesId",
                table: "Games",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Series_BracketId",
                table: "Series",
                column: "BracketId");

            migrationBuilder.CreateIndex(
                name: "IX_Series_Player1Id",
                table: "Series",
                column: "Player1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Series_Player2Id",
                table: "Series",
                column: "Player2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Series_Player3Id",
                table: "Series",
                column: "Player3Id");

            migrationBuilder.CreateIndex(
                name: "IX_Series_Player4Id",
                table: "Series",
                column: "Player4Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Brackets_BracketId",
                table: "Games",
                column: "BracketId",
                principalTable: "Brackets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Series_SeriesId",
                table: "Games",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_Clubs_ClubId",
                table: "Tournaments",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Brackets_BracketId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Series_SeriesId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_Clubs_ClubId",
                table: "Tournaments");

            migrationBuilder.DropTable(
                name: "Series");

            migrationBuilder.DropIndex(
                name: "IX_Tournaments_ClubId",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_Games_SeriesId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "LogLink",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "SeriesId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Player1_EndScore",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Player1_MatchResult",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Player1_RunningTotal",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Player1_Seat",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Player2_EndScore",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Player2_MatchResult",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Player2_RunningTotal",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Player2_Seat",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Player3_EndScore",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Player3_MatchResult",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Player3_RunningTotal",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Player3_Seat",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Player4_EndScore",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Player4_MatchResult",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Player4_RunningTotal",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Player4_Seat",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Placement",
                table: "BracketPlayers");

            migrationBuilder.AlterColumn<int>(
                name: "BracketId",
                table: "Games",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<float>(
                name: "EndScoreNan",
                table: "Games",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "EndScorePei",
                table: "Games",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "EndScoreSha",
                table: "Games",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "EndScoreTon",
                table: "Games",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "MatchResultNan",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MatchResultPei",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MatchResultSha",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MatchResultTon",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NanchaId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PeichaId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Series",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShachaId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TonchaId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Brackets_BracketId",
                table: "Games",
                column: "BracketId",
                principalTable: "Brackets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_BracketPlayers_NanchaId",
                table: "Games",
                column: "NanchaId",
                principalTable: "BracketPlayers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_BracketPlayers_PeichaId",
                table: "Games",
                column: "PeichaId",
                principalTable: "BracketPlayers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_BracketPlayers_ShachaId",
                table: "Games",
                column: "ShachaId",
                principalTable: "BracketPlayers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_BracketPlayers_TonchaId",
                table: "Games",
                column: "TonchaId",
                principalTable: "BracketPlayers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

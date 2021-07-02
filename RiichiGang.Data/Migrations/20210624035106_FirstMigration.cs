using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RiichiGang.Data.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    Stats_TotalGames = table.Column<int>(nullable: true),
                    Stats_TotalRounds = table.Column<int>(nullable: true),
                    Stats_FirstPlaces = table.Column<int>(nullable: true),
                    Stats_SecondPlaces = table.Column<int>(nullable: true),
                    Stats_ThirdPlaces = table.Column<int>(nullable: true),
                    Stats_FourthPlaces = table.Column<int>(nullable: true),
                    Stats_TotalBusted = table.Column<int>(nullable: true),
                    Stats_WinRounds = table.Column<int>(nullable: true),
                    Stats_TsumoRounds = table.Column<int>(nullable: true),
                    Stats_CallRounds = table.Column<int>(nullable: true),
                    Stats_RiichiRounds = table.Column<int>(nullable: true),
                    Stats_DealInRounds = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clubs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true),
                    Contact = table.Column<string>(nullable: true),
                    Localization = table.Column<string>(nullable: true),
                    OwnerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clubs_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Memberships",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    ClubId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memberships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Memberships_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Memberships_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rulesets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ClubId = table.Column<int>(nullable: false),
                    Mochiten = table.Column<int>(nullable: false),
                    Genten = table.Column<int>(nullable: false),
                    UmaFirst = table.Column<int>(nullable: false),
                    UmaSecond = table.Column<int>(nullable: false),
                    UmaThird = table.Column<int>(nullable: false),
                    UmaFourth = table.Column<int>(nullable: false),
                    Oka = table.Column<int>(nullable: false),
                    Atozuke = table.Column<bool>(nullable: false),
                    Kuitan = table.Column<bool>(nullable: false),
                    Kuikae = table.Column<int>(nullable: false),
                    UraDora = table.Column<bool>(nullable: false),
                    KanDora = table.Column<bool>(nullable: false),
                    KanUraDora = table.Column<bool>(nullable: false),
                    AkaDora = table.Column<bool>(nullable: false),
                    AgariYame = table.Column<bool>(nullable: false),
                    TenpaiYame = table.Column<bool>(nullable: false),
                    Tobi = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rulesets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rulesets_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    AllowNonMembers = table.Column<bool>(nullable: false),
                    RequirePermission = table.Column<bool>(nullable: false),
                    ClubId = table.Column<int>(nullable: false),
                    RulesetId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tournaments_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tournaments_Rulesets_RulesetId",
                        column: x => x.RulesetId,
                        principalTable: "Rulesets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tournaments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                    StartedAt = table.Column<DateTime>(nullable: true),
                    FinishedAt = table.Column<DateTime>(nullable: true),
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
                    Placement = table.Column<int>(nullable: false),
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
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    CreatorId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    MembershipId = table.Column<int>(nullable: true),
                    TournamentPlayerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_Memberships_MembershipId",
                        column: x => x.MembershipId,
                        principalTable: "Memberships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notifications_TournamentPlayers_TournamentPlayerId",
                        column: x => x.TournamentPlayerId,
                        principalTable: "TournamentPlayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    SeriesId = table.Column<int>(nullable: false),
                    Player1_Seat = table.Column<int>(nullable: true),
                    Player1_MatchResult = table.Column<int>(nullable: true),
                    Player1_EndScore = table.Column<float>(nullable: true),
                    Player1_RunningTotal = table.Column<float>(nullable: true),
                    Player2_Seat = table.Column<int>(nullable: true),
                    Player2_MatchResult = table.Column<int>(nullable: true),
                    Player2_EndScore = table.Column<float>(nullable: true),
                    Player2_RunningTotal = table.Column<float>(nullable: true),
                    Player3_Seat = table.Column<int>(nullable: true),
                    Player3_MatchResult = table.Column<int>(nullable: true),
                    Player3_EndScore = table.Column<float>(nullable: true),
                    Player3_RunningTotal = table.Column<float>(nullable: true),
                    Player4_Seat = table.Column<int>(nullable: true),
                    Player4_MatchResult = table.Column<int>(nullable: true),
                    Player4_EndScore = table.Column<float>(nullable: true),
                    Player4_RunningTotal = table.Column<float>(nullable: true),
                    PlayedAt = table.Column<DateTime>(nullable: true),
                    LogLink = table.Column<string>(nullable: true),
                    LogFile = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_Clubs_Name",
                table: "Clubs",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_OwnerId",
                table: "Clubs",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_SeriesId",
                table: "Games",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_ClubId",
                table: "Memberships",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_UserId",
                table: "Memberships",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CreatorId",
                table: "Notifications",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_MembershipId",
                table: "Notifications",
                column: "MembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_TournamentPlayerId",
                table: "Notifications",
                column: "TournamentPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rulesets_ClubId_Name",
                table: "Rulesets",
                columns: new[] { "ClubId", "Name" },
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_TournamentPlayers_TournamentId",
                table: "TournamentPlayers",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentPlayers_UserId",
                table: "TournamentPlayers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_ClubId",
                table: "Tournaments",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_RulesetId",
                table: "Tournaments",
                column: "RulesetId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_UserId",
                table: "Tournaments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Series");

            migrationBuilder.DropTable(
                name: "Memberships");

            migrationBuilder.DropTable(
                name: "BracketPlayers");

            migrationBuilder.DropTable(
                name: "Brackets");

            migrationBuilder.DropTable(
                name: "TournamentPlayers");

            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.DropTable(
                name: "Rulesets");

            migrationBuilder.DropTable(
                name: "Clubs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

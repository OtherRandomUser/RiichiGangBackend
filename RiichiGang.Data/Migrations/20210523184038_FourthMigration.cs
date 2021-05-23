using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RiichiGang.Data.Migrations
{
    public partial class FourthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_Rulesets_ClubId",
                table: "Rulesets",
                column: "ClubId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rulesets");
        }
    }
}

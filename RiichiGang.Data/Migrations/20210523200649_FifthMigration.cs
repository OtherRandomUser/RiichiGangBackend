using Microsoft.EntityFrameworkCore.Migrations;

namespace RiichiGang.Data.Migrations
{
    public partial class FifthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rulesets_ClubId",
                table: "Rulesets");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Rulesets",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rulesets_ClubId_Name",
                table: "Rulesets",
                columns: new[] { "ClubId", "Name" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rulesets_ClubId_Name",
                table: "Rulesets");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Rulesets",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rulesets_ClubId",
                table: "Rulesets",
                column: "ClubId");
        }
    }
}

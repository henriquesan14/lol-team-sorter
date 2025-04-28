using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoLTeamSorter.Infra.Migrations
{
    /// <inheritdoc />
    public partial class VictoriesMigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Victories",
                table: "Players",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "WinningTeamId",
                table: "Matchmakings",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matchmakings_WinningTeamId",
                table: "Matchmakings",
                column: "WinningTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matchmakings_Teams_WinningTeamId",
                table: "Matchmakings",
                column: "WinningTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matchmakings_Teams_WinningTeamId",
                table: "Matchmakings");

            migrationBuilder.DropIndex(
                name: "IX_Matchmakings_WinningTeamId",
                table: "Matchmakings");

            migrationBuilder.DropColumn(
                name: "Victories",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "WinningTeamId",
                table: "Matchmakings");
        }
    }
}

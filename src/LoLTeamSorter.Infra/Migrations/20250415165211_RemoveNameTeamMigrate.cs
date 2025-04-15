using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoLTeamSorter.Infra.Migrations
{
    /// <inheritdoc />
    public partial class RemoveNameTeamMigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Teams");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Teams",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoLTeamSorter.Infra.Migrations
{
    /// <inheritdoc />
    public partial class CreateByMigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // USERS
            migrationBuilder.AddColumn<Guid>("CreatedBy_temp", "Users", "uuid", nullable: true);
            migrationBuilder.AddColumn<Guid>("LastModifiedBy_temp", "Users", "uuid", nullable: true);
            migrationBuilder.Sql(@"UPDATE ""Users"" SET ""CreatedBy_temp"" = NULLIF(""CreatedBy"", '')::uuid;");
            migrationBuilder.Sql(@"UPDATE ""Users"" SET ""LastModifiedBy_temp"" = NULLIF(""LastModifiedBy"", '')::uuid;");
            migrationBuilder.DropColumn("CreatedBy", "Users");
            migrationBuilder.DropColumn("LastModifiedBy", "Users");
            migrationBuilder.RenameColumn("CreatedBy_temp", "Users", "CreatedBy");
            migrationBuilder.RenameColumn("LastModifiedBy_temp", "Users", "LastModifiedBy");

            // TEAMS
            migrationBuilder.AddColumn<Guid>("CreatedBy_temp", "Teams", "uuid", nullable: true);
            migrationBuilder.AddColumn<Guid>("LastModifiedBy_temp", "Teams", "uuid", nullable: true);
            migrationBuilder.Sql(@"UPDATE ""Teams"" SET ""CreatedBy_temp"" = NULLIF(""CreatedBy"", '')::uuid;");
            migrationBuilder.Sql(@"UPDATE ""Teams"" SET ""LastModifiedBy_temp"" = NULLIF(""LastModifiedBy"", '')::uuid;");
            migrationBuilder.DropColumn("CreatedBy", "Teams");
            migrationBuilder.DropColumn("LastModifiedBy", "Teams");
            migrationBuilder.RenameColumn("CreatedBy_temp", "Teams", "CreatedBy");
            migrationBuilder.RenameColumn("LastModifiedBy_temp", "Teams", "LastModifiedBy");

            // PLAYERS
            migrationBuilder.AddColumn<Guid>("CreatedBy_temp", "Players", "uuid", nullable: true);
            migrationBuilder.AddColumn<Guid>("LastModifiedBy_temp", "Players", "uuid", nullable: true);
            migrationBuilder.Sql(@"UPDATE ""Players"" SET ""CreatedBy_temp"" = NULLIF(""CreatedBy"", '')::uuid;");
            migrationBuilder.Sql(@"UPDATE ""Players"" SET ""LastModifiedBy_temp"" = NULLIF(""LastModifiedBy"", '')::uuid;");
            migrationBuilder.DropColumn("CreatedBy", "Players");
            migrationBuilder.DropColumn("LastModifiedBy", "Players");
            migrationBuilder.RenameColumn("CreatedBy_temp", "Players", "CreatedBy");
            migrationBuilder.RenameColumn("LastModifiedBy_temp", "Players", "LastModifiedBy");

            // PERMISSIONS
            migrationBuilder.AddColumn<Guid>("CreatedBy_temp", "Permissions", "uuid", nullable: true);
            migrationBuilder.AddColumn<Guid>("LastModifiedBy_temp", "Permissions", "uuid", nullable: true);
            migrationBuilder.Sql(@"UPDATE ""Permissions"" SET ""CreatedBy_temp"" = NULLIF(""CreatedBy"", '')::uuid;");
            migrationBuilder.Sql(@"UPDATE ""Permissions"" SET ""LastModifiedBy_temp"" = NULLIF(""LastModifiedBy"", '')::uuid;");
            migrationBuilder.DropColumn("CreatedBy", "Permissions");
            migrationBuilder.DropColumn("LastModifiedBy", "Permissions");
            migrationBuilder.RenameColumn("CreatedBy_temp", "Permissions", "CreatedBy");
            migrationBuilder.RenameColumn("LastModifiedBy_temp", "Permissions", "LastModifiedBy");

            // MATCHMAKINGS
            migrationBuilder.AddColumn<Guid>("CreatedBy_temp", "Matchmakings", "uuid", nullable: true);
            migrationBuilder.AddColumn<Guid>("LastModifiedBy_temp", "Matchmakings", "uuid", nullable: true);
            migrationBuilder.Sql(@"UPDATE ""Matchmakings"" SET ""CreatedBy_temp"" = NULLIF(""CreatedBy"", '')::uuid;");
            migrationBuilder.Sql(@"UPDATE ""Matchmakings"" SET ""LastModifiedBy_temp"" = NULLIF(""LastModifiedBy"", '')::uuid;");
            migrationBuilder.DropColumn("CreatedBy", "Matchmakings");
            migrationBuilder.DropColumn("LastModifiedBy", "Matchmakings");
            migrationBuilder.RenameColumn("CreatedBy_temp", "Matchmakings", "CreatedBy");
            migrationBuilder.RenameColumn("LastModifiedBy_temp", "Matchmakings", "LastModifiedBy");

            // GROUPS
            migrationBuilder.AddColumn<Guid>("CreatedBy_temp", "Groups", "uuid", nullable: true);
            migrationBuilder.AddColumn<Guid>("LastModifiedBy_temp", "Groups", "uuid", nullable: true);
            migrationBuilder.Sql(@"UPDATE ""Groups"" SET ""CreatedBy_temp"" = NULLIF(""CreatedBy"", '')::uuid;");
            migrationBuilder.Sql(@"UPDATE ""Groups"" SET ""LastModifiedBy_temp"" = NULLIF(""LastModifiedBy"", '')::uuid;");
            migrationBuilder.DropColumn("CreatedBy", "Groups");
            migrationBuilder.DropColumn("LastModifiedBy", "Groups");
            migrationBuilder.RenameColumn("CreatedBy_temp", "Groups", "CreatedBy");
            migrationBuilder.RenameColumn("LastModifiedBy_temp", "Groups", "LastModifiedBy");
        }
    }
}

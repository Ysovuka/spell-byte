using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpellByte.Migrations.EffectDb
{
    public partial class AddSpeeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NatureId",
                table: "Effects",
                newName: "RecoverySpeedId");

            migrationBuilder.AddColumn<Guid>(
                name: "CastingSpeedId",
                table: "Effects",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RecastSpeedId",
                table: "Effects",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CastingSpeedId",
                table: "Effects");

            migrationBuilder.DropColumn(
                name: "RecastSpeedId",
                table: "Effects");

            migrationBuilder.RenameColumn(
                name: "RecoverySpeedId",
                table: "Effects",
                newName: "NatureId");
        }
    }
}

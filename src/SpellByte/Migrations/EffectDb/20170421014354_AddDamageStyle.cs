using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpellByte.Migrations.EffectDb
{
    public partial class AddDamageStyle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "DamagePerTick",
                table: "Effects",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "Damage",
                table: "Effects",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "DamagePerTickStyle",
                table: "Effects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DamageStyle",
                table: "Effects",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DamagePerTickStyle",
                table: "Effects");

            migrationBuilder.DropColumn(
                name: "DamageStyle",
                table: "Effects");

            migrationBuilder.AlterColumn<int>(
                name: "DamagePerTick",
                table: "Effects",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "Damage",
                table: "Effects",
                nullable: false,
                oldClrType: typeof(float));
        }
    }
}

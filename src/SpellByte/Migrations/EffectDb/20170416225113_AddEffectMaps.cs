using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpellByte.Migrations.EffectDb
{
    public partial class AddEffectMaps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EffectMaps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Effect = table.Column<Guid>(nullable: false),
                    Key = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EffectMaps", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EffectMaps");
        }
    }
}

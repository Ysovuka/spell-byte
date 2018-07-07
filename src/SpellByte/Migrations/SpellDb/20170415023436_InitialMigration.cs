using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SpellByte.Migrations.SpellDb
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Spells",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CastingSpeed = table.Column<Guid>(nullable: false),
                    Code = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Domain = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Radius = table.Column<float>(nullable: false),
                    Range = table.Column<float>(nullable: false),
                    RecastSpeed = table.Column<Guid>(nullable: false),
                    RecoverySpeed = table.Column<Guid>(nullable: false),
                    Shape = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spells", x => x.Id);
                    table.UniqueConstraint("AK_Spells_Code", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "SpellMaps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Effect = table.Column<Guid>(nullable: false),
                    Spell = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpellMaps", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Spells");

            migrationBuilder.DropTable(
                name: "SpellMaps");
        }
    }
}

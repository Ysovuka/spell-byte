using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SpellByte.Internal;
using SpellByte;

namespace SpellByte.Migrations.SpellDb
{
    [DbContext(typeof(SpellDbContext))]
    partial class SpellDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SpellByte.Spell", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CastingSpeed");

                    b.Property<int>("Code")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<Guid>("Domain");

                    b.Property<string>("Name");

                    b.Property<float>("Radius");

                    b.Property<float>("Range");

                    b.Property<Guid>("RecastSpeed");

                    b.Property<Guid>("RecoverySpeed");

                    b.Property<int>("Shape");

                    b.HasKey("Id");

                    b.HasAlternateKey("Code");

                    b.ToTable("Spells");
                });

            modelBuilder.Entity("SpellByte.SpellMap", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("Effect");

                    b.Property<Guid>("Spell");

                    b.HasKey("Id");

                    b.ToTable("SpellMaps");
                });
        }
    }
}

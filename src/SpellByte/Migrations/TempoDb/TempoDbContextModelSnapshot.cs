using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SpellByte.Internal;
using SpellByte;

namespace SpellByte.Migrations.TempoDb
{
    [DbContext(typeof(TempoDbContext))]
    partial class TempoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SpellByte.Tempo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Code")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Timing");

                    b.Property<int>("Value");

                    b.HasKey("Id");

                    b.HasAlternateKey("Code");

                    b.ToTable("Tempos");
                });
        }
    }
}

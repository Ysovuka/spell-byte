using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SpellByte.Internal;
using SpellByte;

namespace SpellByte.Migrations.EffectDb
{
    [DbContext(typeof(EffectDbContext))]
    [Migration("20170415191048_AddNatureTypes")]
    partial class AddNatureTypes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SpellByte.Effect", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AfflictionId");

                    b.Property<int>("Code")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<Guid>("DurationId");

                    b.Property<Guid>("InflictionId");

                    b.Property<string>("Name");

                    b.Property<int>("Nature");

                    b.Property<Guid>("NatureId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasAlternateKey("Code");

                    b.ToTable("Effects");
                });
        }
    }
}

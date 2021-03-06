﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SpellByte.Internal;
using SpellByte;

namespace SpellByte.Migrations.EffectDb
{
    [DbContext(typeof(EffectDbContext))]
    [Migration("20170421012123_AddDamage")]
    partial class AddDamage
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

                    b.Property<Guid>("CastingSpeedId");

                    b.Property<int>("Category");

                    b.Property<int>("Code")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Damage");

                    b.Property<int>("DamagePerTick");

                    b.Property<string>("Description");

                    b.Property<Guid>("DurationId");

                    b.Property<Guid>("DurationTickId");

                    b.Property<Guid>("ImmunityDurationId");

                    b.Property<Guid>("InflictionId");

                    b.Property<string>("Name");

                    b.Property<int>("Nature");

                    b.Property<Guid>("RecastSpeedId");

                    b.Property<Guid>("RecoverySpeedId");

                    b.Property<int>("Trigger");

                    b.Property<int>("TriggerCount");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasAlternateKey("Code");

                    b.ToTable("Effects");
                });

            modelBuilder.Entity("SpellByte.EffectMap", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("Effect");

                    b.Property<Guid>("Key");

                    b.HasKey("Id");

                    b.ToTable("EffectMaps");
                });
        }
    }
}

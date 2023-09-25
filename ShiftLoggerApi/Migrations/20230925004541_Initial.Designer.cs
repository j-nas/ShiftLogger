﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShiftLoggerApi.Data;

#nullable disable

namespace ShiftLoggerApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230925004541_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ShiftLoggerApi.Models.Shift", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<long>("WorkerId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("WorkerId");

                    b.ToTable("Shifts");
                });

            modelBuilder.Entity("ShiftLoggerApi.Models.Worker", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("ShiftLoggerApi.Models.Shift", b =>
                {
                    b.HasOne("ShiftLoggerApi.Models.Worker", "Worker")
                        .WithMany("Shifts")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("ShiftLoggerApi.Models.Worker", b =>
                {
                    b.Navigation("Shifts");
                });
#pragma warning restore 612, 618
        }
    }
}

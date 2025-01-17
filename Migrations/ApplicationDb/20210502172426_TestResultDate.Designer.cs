﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Typings.Data;

namespace Typings.Migrations.ApplicationDb
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210502172426_TestResultDate")]
    partial class TestResultDate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("Typings.Data.TestResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Accuracy")
                        .HasColumnType("int");

                    b.Property<string>("AuthorUsername")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("Date")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Wpm")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorUsername");

                    b.ToTable("TestResults");
                });

            modelBuilder.Entity("Typings.Data.User", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.HasKey("Username");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Typings.Data.TestResult", b =>
                {
                    b.HasOne("Typings.Data.User", "Author")
                        .WithMany("TestResults")
                        .HasForeignKey("AuthorUsername");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Typings.Data.User", b =>
                {
                    b.Navigation("TestResults");
                });
#pragma warning restore 612, 618
        }
    }
}

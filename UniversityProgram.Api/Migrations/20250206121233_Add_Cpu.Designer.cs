﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UniversityProgram.Api;

#nullable disable

namespace UniversityProgram.Api.Migrations
{
    [DbContext(typeof(StudentDbContext))]
    [Migration("20250206121233_Add_Cpu")]
    partial class Add_Cpu
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UniversityProgram.Api.Entities.Cpu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("LaptopId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("LaptopId")
                        .IsUnique();

                    b.ToTable("Cpu");
                });

            modelBuilder.Entity("UniversityProgram.Api.Entities.Laptop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.ToTable("Laptops");
                });

            modelBuilder.Entity("UniversityProgram.Api.Entities.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Students");
                });

            modelBuilder.Entity("UniversityProgram.Api.Entities.Cpu", b =>
                {
                    b.HasOne("UniversityProgram.Api.Entities.Laptop", "Laptop")
                        .WithOne("Cpu")
                        .HasForeignKey("UniversityProgram.Api.Entities.Cpu", "LaptopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Laptop");
                });

            modelBuilder.Entity("UniversityProgram.Api.Entities.Laptop", b =>
                {
                    b.HasOne("UniversityProgram.Api.Entities.Student", "Student")
                        .WithOne("Laptop")
                        .HasForeignKey("UniversityProgram.Api.Entities.Laptop", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("UniversityProgram.Api.Entities.Laptop", b =>
                {
                    b.Navigation("Cpu");
                });

            modelBuilder.Entity("UniversityProgram.Api.Entities.Student", b =>
                {
                    b.Navigation("Laptop");
                });
#pragma warning restore 612, 618
        }
    }
}

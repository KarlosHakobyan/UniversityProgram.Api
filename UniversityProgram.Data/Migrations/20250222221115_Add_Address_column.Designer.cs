﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UniversityProgram.Data;

#nullable disable

namespace UniversityProgram.Api.Migrations
{
    [DbContext(typeof(StudentDbContext))]
    [Migration("20250222221115_Add_Address_column")]
    partial class Add_Address_column
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StudentBaseUniversity", b =>
                {
                    b.Property<int>("StudentsId")
                        .HasColumnType("int");

                    b.Property<int>("UniversitiesId")
                        .HasColumnType("int");

                    b.HasKey("StudentsId", "UniversitiesId");

                    b.HasIndex("UniversitiesId");

                    b.ToTable("StudentBaseUniversity");
                });

            modelBuilder.Entity("UniversityProgram.Data.Entities.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Fee")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("UniversityProgram.Data.Entities.CourseStudent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<bool>("Paid")
                        .HasColumnType("bit");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("CourseStudent");
                });

            modelBuilder.Entity("UniversityProgram.Data.Entities.Cpu", b =>
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

            modelBuilder.Entity("UniversityProgram.Data.Entities.Laptop", b =>
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

            modelBuilder.Entity("UniversityProgram.Data.Entities.Library", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Libraries");
                });

            modelBuilder.Entity("UniversityProgram.Data.Entities.StudentBase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Age")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar");

                    b.Property<int?>("LibraryId")
                        .HasColumnType("int");

                    b.Property<decimal>("Money")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("LibraryId");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Monument",
                            Age = 0L,
                            Email = "Kar@mail.ru",
                            Money = 0m,
                            Name = "Karen"
                        });
                });

            modelBuilder.Entity("UniversityProgram.Data.Entities.University", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Universities");
                });

            modelBuilder.Entity("StudentBaseUniversity", b =>
                {
                    b.HasOne("UniversityProgram.Data.Entities.StudentBase", null)
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniversityProgram.Data.Entities.University", null)
                        .WithMany()
                        .HasForeignKey("UniversitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UniversityProgram.Data.Entities.CourseStudent", b =>
                {
                    b.HasOne("UniversityProgram.Data.Entities.Course", "Course")
                        .WithMany("CourseStudents")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniversityProgram.Data.Entities.StudentBase", "Student")
                        .WithMany("CourseStudents")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("UniversityProgram.Data.Entities.Cpu", b =>
                {
                    b.HasOne("UniversityProgram.Data.Entities.Laptop", "Laptop")
                        .WithOne("Cpu")
                        .HasForeignKey("UniversityProgram.Data.Entities.Cpu", "LaptopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Laptop");
                });

            modelBuilder.Entity("UniversityProgram.Data.Entities.Laptop", b =>
                {
                    b.HasOne("UniversityProgram.Data.Entities.StudentBase", "Student")
                        .WithOne("Laptop")
                        .HasForeignKey("UniversityProgram.Data.Entities.Laptop", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("UniversityProgram.Data.Entities.StudentBase", b =>
                {
                    b.HasOne("UniversityProgram.Data.Entities.Library", "Library")
                        .WithMany("Students")
                        .HasForeignKey("LibraryId");

                    b.Navigation("Library");
                });

            modelBuilder.Entity("UniversityProgram.Data.Entities.Course", b =>
                {
                    b.Navigation("CourseStudents");
                });

            modelBuilder.Entity("UniversityProgram.Data.Entities.Laptop", b =>
                {
                    b.Navigation("Cpu");
                });

            modelBuilder.Entity("UniversityProgram.Data.Entities.Library", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("UniversityProgram.Data.Entities.StudentBase", b =>
                {
                    b.Navigation("CourseStudents");

                    b.Navigation("Laptop");
                });
#pragma warning restore 612, 618
        }
    }
}

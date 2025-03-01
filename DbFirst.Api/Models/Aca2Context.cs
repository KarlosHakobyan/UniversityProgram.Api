using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DbFirst.Api.Models;

public partial class Aca2Context : DbContext
{
    public Aca2Context()
    {
    }

    public Aca2Context(DbContextOptions<Aca2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseStudent> CourseStudents { get; set; }

    public virtual DbSet<Cpu> Cpus { get; set; }

    public virtual DbSet<Laptop> Laptops { get; set; }

    public virtual DbSet<Library> Libraries { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<University> Universities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-SUH9IQ0;Database=ACA2;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.Property(e => e.Fee).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<CourseStudent>(entity =>
        {
            entity.ToTable("CourseStudent");

            entity.HasIndex(e => e.CourseId, "IX_CourseStudent_CourseId");

            entity.HasIndex(e => e.StudentId, "IX_CourseStudent_StudentId");

            entity.Property(e => e.Paid)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");

            entity.HasOne(d => d.Course).WithMany(p => p.CourseStudents).HasForeignKey(d => d.CourseId);

            entity.HasOne(d => d.Student).WithMany(p => p.CourseStudents).HasForeignKey(d => d.StudentId);
        });

        modelBuilder.Entity<Cpu>(entity =>
        {
            entity.ToTable("Cpu");

            entity.HasIndex(e => e.LaptopId, "IX_Cpu_LaptopId").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Laptop).WithOne(p => p.Cpu).HasForeignKey<Cpu>(d => d.LaptopId);
        });

        modelBuilder.Entity<Laptop>(entity =>
        {
            entity.HasIndex(e => e.StudentId, "IX_Laptops_StudentId").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Student).WithOne(p => p.Laptop).HasForeignKey<Laptop>(d => d.StudentId);
        });

        modelBuilder.Entity<Library>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasIndex(e => e.Email, "IX_Students_Email").IsUnique();

            entity.HasIndex(e => e.LibraryId, "IX_Students_LibraryId");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Money).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Library).WithMany(p => p.Students).HasForeignKey(d => d.LibraryId);

            entity.HasMany(d => d.Universities).WithMany(p => p.Students)
                .UsingEntity<Dictionary<string, object>>(
                    "StudentUniversity",
                    r => r.HasOne<University>().WithMany().HasForeignKey("UniversitiesId"),
                    l => l.HasOne<Student>().WithMany().HasForeignKey("StudentsId"),
                    j =>
                    {
                        j.HasKey("StudentsId", "UniversitiesId");
                        j.ToTable("StudentUniversity");
                        j.HasIndex(new[] { "UniversitiesId" }, "IX_StudentUniversity_UniversitiesId");
                    });
        });

        modelBuilder.Entity<University>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

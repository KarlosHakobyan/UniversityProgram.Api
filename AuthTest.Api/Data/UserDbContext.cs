using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AuthTest.Api.Data
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = default!;

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasKey(e => e.Id);
            modelBuilder.Entity<User>()
            .Property(e => e.Email)
            .HasColumnType("nvarchar")
            .HasMaxLength(50)
            .IsRequired();

            modelBuilder.Entity<User>()
            .HasIndex(e => e.Email).IsUnique();

            modelBuilder.Entity<User>()
                .Property(e => e.Role).HasColumnType("nvarchar").HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(e => e.PasswordHash).IsRequired();
        }
    }

    public class User
    {
        public int Id { get; set; }
        [EmailAddress]
        public string Email { get; set; } = null!;
        public string? Role { get; set; }
        public string? PasswordHash { get; set; }
    }
}
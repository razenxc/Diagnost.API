using Microsoft.EntityFrameworkCore;
using Diagnost.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Diagnost.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Teacher> Teachers { get; set; } = null!;
        public DbSet<Session> Sessions { get; set; } = null!;
        public DbSet<Result> Results { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("Teachers");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Login).IsRequired().HasMaxLength(200);
                entity.HasIndex(e => e.Login).IsUnique();
                entity.Property(e => e.PasswordHash).IsRequired();
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.ToTable("Sessions");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Code).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.Code).IsUnique();
                entity.Property(e => e.TestType).IsRequired();
                entity.Property(e => e.IsActive).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.HasOne(e => e.Teacher)
                      .WithMany()
                      .HasForeignKey(e => e.TeacherId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Result>(entity =>
            {
                entity.ToTable("Results");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.StudentName).IsRequired().HasMaxLength(300);
                entity.Property(e => e.AverageTime).IsRequired();
                entity.Property(e => e.ErrorsCount).IsRequired();
                entity.Property(e => e.RawDataJson).HasColumnType("text");
                entity.Property(e => e.SubmittedAt).IsRequired();
                entity.HasOne(e => e.Session)
                      .WithMany()
                      .HasForeignKey(e => e.SessionId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Note: do not perform user manager operations here to avoid DI circular dependency.
        }
    }
}

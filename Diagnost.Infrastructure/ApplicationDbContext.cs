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

        public DbSet<AccessCode> AccessCodes { get; set; } = null!;
        public DbSet<Result> Results { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AccessCode>(entity =>
            {
                entity.ToTable("Sessions");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Code).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.Code).IsUnique();
                entity.Property(e => e.IsActive).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired();
            });

            modelBuilder.Entity<Result>(entity =>
            {
                entity.ToTable("Results");
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.AccessCode)
                      .WithMany()
                      .HasForeignKey(e => e.AccessCodeId)
                      .OnDelete(DeleteBehavior.Cascade); // If AccessCode is deleted, delete related Results
                entity.Property(e => e.StudentFullName)
                      .IsRequired()
                      .HasMaxLength(250);
                entity.Property(e => e.Group)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.Property(e => e.SportType)
                      .HasMaxLength(250);
                entity.Property(e => e.SportQualification)
                      .HasMaxLength(100);
                entity.Property(e => e.Gender)
                      .HasMaxLength(50);
                entity.Property(e => e.SubmittedAt)
                      .IsRequired();
                entity.Property(e => e.PZMRLatet).IsRequired();
                entity.Property(e => e.PZMRvidhil).IsRequired();
                entity.Property(e => e.PZMR_ErrorsTotal).IsRequired();
                entity.Property(e => e.PV2_3Latet).IsRequired();
                entity.Property(e => e.PV2_StdDev_ms).IsRequired();
                entity.Property(e => e.PV2_ErrorsMissed).IsRequired();
                entity.Property(e => e.PV2_ErrorsWrongButton).IsRequired();
                entity.Property(e => e.PV2_ErrorsFalseAlarm).IsRequired();
                entity.Property(e => e.UFPLatet).IsRequired();
                entity.Property(e => e.UFP_StdDev_ms).IsRequired();
                entity.Property(e => e.UFP_ErrorsTotal).IsRequired();
            });
        }
    }
}

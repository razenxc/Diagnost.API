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
        public DbSet<AccessCode> AccessCodes { get; set; } = null!;
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
                      .HasMaxLength(150);
                entity.Property(e => e.Group)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.Property(e => e.SportType)
                      .HasMaxLength(100);
                entity.Property(e => e.SportQualification)
                      .HasMaxLength(100);
                entity.Property(e => e.Gender)
                      .HasMaxLength(20);
                entity.Property(e => e.SubmittedAt)
                      .IsRequired();
                entity.Property(e => e.PZMRChtoToTam1).IsRequired();
                entity.Property(e => e.PZMRSmth2).IsRequired();
                entity.Property(e => e.PZMR_ErrorsTotal).IsRequired();
                entity.Property(e => e.PZMR_SuccessfulClicks).IsRequired();
                entity.Property(e => e.PV2_3Smth1).IsRequired();
                entity.Property(e => e.PV2_StdDev_ms).IsRequired();
                entity.Property(e => e.PV2_ErrorsMissed).IsRequired();
                entity.Property(e => e.PV2_ErrorsWrongButton).IsRequired();
                entity.Property(e => e.PV2_ErrorsFalseAlarm).IsRequired();
                entity.Property(e => e.UFPSmth1).IsRequired();
                entity.Property(e => e.UFP_StdDev_ms).IsRequired();
                entity.Property(e => e.UFP_MinExposure_ms).IsRequired();
                entity.Property(e => e.UFP_TotalTime_s).IsRequired();
                entity.Property(e => e.UFP_TimeTillMinExp_s).IsRequired();
                entity.Property(e => e.UFP_ErrorsMissed).IsRequired();
                entity.Property(e => e.UFP_ErrorsWrongButton).IsRequired();
                entity.Property(e => e.UFP_ErrorsFalseAlarm).IsRequired();
            });
        }
    }
}

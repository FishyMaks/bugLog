using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace bugLog.Models
{
    public partial class BugDbContext :DbContext
    {
        public BugDbContext(DbContextOptions<BugDbContext> opts)
            : base(opts)
        {
        }

        public virtual DbSet<Bug> Bugs { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<UserAccess> UserAccesses { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bug>(entity =>
            {
                entity.HasKey(e => e.BugId);

                entity.Property(e => e.BugId).ValueGeneratedNever();

                entity.Property(e => e.BugDescription).IsRequired();

                entity.Property(e => e.BugReportedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.BugResolutionSummary).IsRequired();

                entity.Property(e => e.BugSeverity)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.BugStatus)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.BugTitle)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.BugAssignedToNavigation)
                    .WithMany(p => p.BugsBugAssignedToNavigation)
                    .HasForeignKey(d => d.BugAssignedTo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bugs(Assigned)_UserProfile");

                entity.HasOne(d => d.BugClosedByNavigation)
                    .WithMany(p => p.BugsBugClosedByNavigation)
                    .HasForeignKey(d => d.BugClosedBy)
                    .HasConstraintName("FK_Bugs(Closed)_UserProfile");

                entity.HasOne(d => d.BugCreatedByNavigation)
                    .WithMany(p => p.BugsBugCreatedByNavigation)
                    .HasForeignKey(d => d.BugCreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bugs(Created)_UserProfile");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(e => e.TeamId);

                entity.Property(e => e.TeamId).ValueGeneratedNever();

                entity.Property(e => e.TeamMotto).HasMaxLength(50);

                entity.Property(e => e.TeamName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UserAccess>(entity =>
            {
                entity.HasKey(e => e.TeamId)
                    .HasName("PK_Access");

                entity.Property(e => e.TeamId).ValueGeneratedNever();
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_UserProfile");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired();

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.UserProfiles)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserProfile_Teams");

                entity.HasOne(d => d.TeamNavigation)
                    .WithMany(p => p.UserProfiles)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserProfile_Access");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

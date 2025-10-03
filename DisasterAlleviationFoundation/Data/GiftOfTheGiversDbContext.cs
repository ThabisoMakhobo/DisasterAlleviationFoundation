using DisasterAlleviationFoundation.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DisasterAlleviationFoundation.Data
{
    public class GiftOfTheGiversDbContext : IdentityDbContext<User>
    {
        public GiftOfTheGiversDbContext(DbContextOptions<GiftOfTheGiversDbContext> options)
            : base(options) { }

        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Crisis> Crises { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Distribution> Distributions { get; set; }
        public DbSet<IncidentReport> IncidentReports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // TaskEntity → Crisis
            modelBuilder.Entity<TaskEntity>()
                .HasOne(t => t.Crisis)
                .WithMany(c => c.Tasks)
                .HasForeignKey(t => t.CrisisID)
                .OnDelete(DeleteBehavior.Restrict); // Avoid multiple cascade paths

            // TaskEntity → Volunteer (optional)
            modelBuilder.Entity<TaskEntity>()
                .HasOne(t => t.Volunteer)
                .WithMany(v => v.Tasks)
                .HasForeignKey(t => t.VolunteerID)
                .OnDelete(DeleteBehavior.Restrict);

            // Donation relationships
            modelBuilder.Entity<Donation>()
                .HasOne(d => d.User)
                .WithMany(u => u.Donations)
                .HasForeignKey(d => d.UserID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Donation>()
                .Property(d => d.Amount)
                .HasColumnType("decimal(18,2)");

            // IncidentReport relationships
            modelBuilder.Entity<IncidentReport>()
                .HasOne(r => r.User)
                .WithMany(u => u.IncidentReports)
                .HasForeignKey(r => r.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Distribution relationships
            modelBuilder.Entity<Distribution>()
                .HasOne(d => d.Crisis)
                .WithMany(c => c.Distributions)
                .HasForeignKey(d => d.CrisisID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Volunteer relationships
            modelBuilder.Entity<Volunteer>()
                .HasOne(v => v.User)
                .WithOne(u => u.Volunteer)
                .HasForeignKey<Volunteer>(v => v.UserID)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}

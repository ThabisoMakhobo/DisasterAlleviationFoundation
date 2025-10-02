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


        protected override void OnModelCreating(ModelBuilder builder)
{
    base.OnModelCreating(builder);

    // TaskEntity configuration
    builder.Entity<TaskEntity>()
        .HasKey(t => t.TaskID); // Ensures EF knows TaskID is the PK

    builder.Entity<TaskEntity>()
        .HasOne(t => t.Crisis)
        .WithMany(c => c.Tasks)
        .HasForeignKey(t => t.CrisisID)
        .OnDelete(DeleteBehavior.Cascade);

    builder.Entity<TaskEntity>()
        .HasOne(t => t.Volunteer)
        .WithMany(v => v.Tasks)
        .HasForeignKey(t => t.VolunteerID)
        .OnDelete(DeleteBehavior.Cascade);

    // Volunteer configuration
    builder.Entity<Volunteer>()
        .HasOne(v => v.User)
        .WithOne(u => u.Volunteer)
        .HasForeignKey<Volunteer>(v => v.UserID)
        .IsRequired();

    // Donation configuration
    builder.Entity<Donation>()
        .HasOne(d => d.User)
        .WithMany(u => u.Donations)
        .HasForeignKey(d => d.UserID)
        .IsRequired();
}

    }
}

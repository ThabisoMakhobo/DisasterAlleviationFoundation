using DisasterAlleviationFoundation.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Volunteer ↔ User (1-to-1)
        modelBuilder.Entity<Volunteer>()
            .HasOne(v => v.User)
            .WithOne(u => u.Volunteer)
            .HasForeignKey<Volunteer>(v => v.UserID)
            .IsRequired();

        // Task ↔ Crisis (many-to-1)
        modelBuilder.Entity<TaskEntity>()
            .HasOne(t => t.Crisis)
            .WithMany(c => c.Tasks)
            .HasForeignKey(t => t.CrisisID);

        // Task ↔ Volunteer (many-to-1)
        modelBuilder.Entity<TaskEntity>()
            .HasOne(t => t.Volunteer)
            .WithMany(v => v.Tasks)
            .HasForeignKey(t => t.VolunteerID);

        // Donation ↔ User (many-to-1)
        modelBuilder.Entity<Donation>()
            .HasOne(d => d.User)
            .WithMany(u => u.Donations)
            .HasForeignKey(d => d.UserID)
            .IsRequired();
    }
}

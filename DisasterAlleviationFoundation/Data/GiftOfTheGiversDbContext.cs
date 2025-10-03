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

    {


            .HasOne(t => t.Crisis)
            .WithMany(c => c.Tasks)

            .HasOne(t => t.Volunteer)
            .WithMany(v => v.Tasks)

            .HasOne(d => d.User)
            .WithMany(u => u.Donations)
            .HasForeignKey(d => d.UserID)
            .IsRequired();
    }

    }
}

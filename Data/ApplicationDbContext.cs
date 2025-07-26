using HelpDesk.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : IdentityDbContext<ApplicationUser , IdentityRole , string>(options)
{
    public DbSet<Ticket> Tickets => Set<Ticket>();
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        //one to many
        builder.Entity<Ticket>()
            .HasOne(x => x.CreatedBy)
            .WithMany(x => x.Tickets)
            .HasForeignKey(x => x.CreatedById);
        builder.Entity<Ticket>().Property(x => x.Description).HasMaxLength(900);
    }

public DbSet<HelpDesk.Entities.Ticket> Ticket { get; set; } = default!;
public DbSet<HelpDesk.Entities.ApplicationUser> ApplicationUsers { get; set; } = default!;
}

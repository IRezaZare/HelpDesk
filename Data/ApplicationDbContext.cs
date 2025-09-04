using HelpDesk.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : IdentityDbContext<ApplicationUser , IdentityRole , string>(options)
{
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<Comment> Comments => Set<Comment>();
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        //one to many
        builder.Entity<Ticket>()
            .HasOne(x => x.CreatedBy)
            .WithMany(x => x.Tickets)
            .HasForeignKey(x => x.CreatedById);
        builder.Entity<Ticket>().Property(x => x.Description).HasMaxLength(900);
        builder.Entity<Ticket>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<Ticket>()
            .ToTable(c =>
            {
                c.IsTemporal(o =>
                {
                    o.UseHistoryTable("TicketHistory");
                    o.HasPeriodStart("From");
                    o.HasPeriodEnd("To");
                });
            });
        //comments
        builder.Entity<Comment>()
            .HasOne(x => x.CreatedBy)
            .WithMany()
            .HasForeignKey(x => x.CreatedById);

        builder.Entity<Comment>()
            .HasOne(x => x.Ticket)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.TicketId);
        builder.Entity<Comment>()
            .Property(x => x.Description).HasMaxLength(500);

    }
   

    public DbSet<HelpDesk.Entities.Ticket> Ticket { get; set; } = default!;
public DbSet<HelpDesk.Entities.ApplicationUser> ApplicationUsers { get; set; } = default!;
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebUI.Models;

namespace WebUI.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUserModel>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<ApplicationUserModel> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasSequence<int>("OrderNumber").StartsAt(1).IncrementsBy(1);
        builder.Entity<ApplicationUserModel>().Property(o => o.OrderId).HasDefaultValueSql("NEXT VALUE FOR OrderNumber");
        base.OnModelCreating(builder);
    }
}

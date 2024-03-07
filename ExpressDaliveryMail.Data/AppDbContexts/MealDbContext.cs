using ExpressDeliveryMail.Domain.Configurations;
using ExpressDeliveryMail.Domain.Entities;
using ExpressDeliveryMail.Domain.Entities.Branches;
using ExpressDeliveryMail.Domain.Entities.Expresses;
using ExpressDeliveryMail.Domain.Entities.Transports;
using ExpressDeliveryMail.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace ExpressDaliveryMail.Data.AppDbContexts;

public class MealDbContext:DbContext
{
    public DbSet<User>users {  get; set; }
    public DbSet<Transport> transports { get; set; }
    public DbSet<Branch> branches { get; set; }
    public DbSet<Transaction> transactions { get; set; }
    public DbSet<Express> expresses { get; set; }
    public DbSet<Payment> payments { get; set; }
    public DbSet<Package> packages { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Constants.ConnectionString);
    }
}
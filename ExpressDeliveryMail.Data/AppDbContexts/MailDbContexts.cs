using ExpressDeliveryMail.Domain.Configurations;
using ExpressDeliveryMail.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpressDeliveryMail.Data.AppDbContexts;

public class MailDbContexts:DbContext
{
    public DbSet<User> users {  get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Constants.ConnectionString);
    }
}

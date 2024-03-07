using ExpressDaliveryMail.Data.AppDbContexts;
using ExpressDaliveryMail.Data.IRepositories;
using ExpressDeliveryMail.Domain.Entities.Transports;
using Microsoft.EntityFrameworkCore;

namespace ExpressDaliveryMail.Data.Repositories;

public class TransportRepository : ITransportRepository
{
    public MealDbContext context;
    public TransportRepository(MealDbContext context)
    {
        this.context = context;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existTransport = await context.transports.FirstAsync(u => u.Id == id && !u.IsDeleted);
        existTransport.IsDeleted = true;
        existTransport.DeletedAt = DateTime.UtcNow;
        context.SaveChanges();
        return true;
    }

    public async ValueTask<IEnumerable<Transport>> GetAllAsync()
    {
        var transports = await context.transports.ToListAsync();
        return transports;
    }

    public async ValueTask<Transport> InsertAsync(Transport transport)
    {
        var createdTransport = await context.transports.AddAsync(transport);
        context.SaveChanges();
        return createdTransport.Entity;
    }

    public async ValueTask<Transport> UpdateAsync(long id, Transport transport)
    {
        var existTransport = await context.transports.FirstAsync(u => u.Id == id);
        existTransport.IsDeleted = false;
        existTransport.Type = transport.Type;
        existTransport.Colour = transport.Colour;
        existTransport.UpdatedAt = DateTime.UtcNow;
        existTransport.Description = transport.Description;
        context.SaveChanges();

        return existTransport;
    }
}

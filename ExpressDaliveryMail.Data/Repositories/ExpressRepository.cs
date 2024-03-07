using ExpressDaliveryMail.Data.AppDbContexts;
using ExpressDaliveryMail.Data.IRepositories;
using ExpressDeliveryMail.Domain.Entities.Expresses;
using Microsoft.EntityFrameworkCore;

namespace ExpressDaliveryMail.Data.Repositories;

public class ExpressRepository : IExpressRepository
{
    public MealDbContext context;
    public ExpressRepository(MealDbContext context)
    {
        this.context = context;
    }
    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existExpress = await context.expresses.FirstAsync(u => u.Id == id && !u.IsDeleted);
        existExpress.IsDeleted = true;
        existExpress.DeletedAt = DateTime.UtcNow;
        context.SaveChanges();
        return true;
    }

    public async ValueTask<IEnumerable<Express>> GetAllAsync()
    {
        var expresses = await context.expresses.ToListAsync();
        return expresses;
    }

    public async ValueTask<Express> InsertAsync(Express express)
    {
        var createdExpess = await context.expresses.AddAsync(express);
        context.SaveChanges();
        return createdExpess.Entity;
    }

    public async ValueTask<Express> UpdateAsync(long id, Express express)
    {
        var existExpress = await context.expresses.FirstAsync(u => u.Id == id);
        existExpress.IsDeleted = false;
        existExpress.BranchId = express.BranchId;
        existExpress.Distance = express.Distance;
        existExpress.UpdatedAt = DateTime.UtcNow;
        existExpress.TransportId = express.TransportId;
        existExpress.ArrivalTime = express.ArrivalTime;
        existExpress.DepartureTime = express.DepartureTime;

        context.SaveChanges();

        return existExpress;
    }
}

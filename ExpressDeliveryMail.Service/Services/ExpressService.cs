using ExpressDaliveryMail.Data.Repositories;
using ExpressDeliveryMail.Domain.Entities.Expresses;
using ExpressDeliveryMail.Service.Extensions;
using ExpressDeliveryMail.Service.Interfaces;

namespace ExpressDeliveryMail.Service.Services;

public class ExpressService : IExpressService
{
    private ExpressRepository expressRepository;
    private BranchService branchService;
    private TransportService transportService;
    public ExpressService(ExpressRepository expressRepository, BranchService branchService, TransportService transportService)
    {
        this.expressRepository = expressRepository;
        this.branchService = branchService;
        this.transportService = transportService;

    }

    public async ValueTask<ExpressViewModel> CreatedAsync(ExpressCreationModel express)
    {
        var existUser = await branchService.GetByIdAsync(express.BranchId);
        var existTransport = await transportService.GetByIdAsync(express.TransportId);

        var expresses = await expressRepository.GetAllAsync();
        var existExpress = expresses.FirstOrDefault(e => e.TransportId == express.TransportId &&  e.DepartureTime == express.DepartureTime);
        if (existExpress is not null)
        {
            if (existExpress.IsDeleted)
                return await UpdateAsync(existExpress.Id, express.MapTo<ExpressUpdateModel>(), true);
            throw new Exception($"This express is already exist With this transport id {express.TransportId}");
        }

        var createdExpress = await expressRepository.InsertAsync(express.MapTo<Express>());
        return createdExpress.MapTo<ExpressViewModel>();
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var expresses = await expressRepository.GetAllAsync();
        var existExpress = expresses.FirstOrDefault(a => a.Id == id && !a.IsDeleted)
            ?? throw new Exception($"This express is not found With this id {id}");

        await expressRepository.DeleteAsync(id);
        return true;
    }

    public async ValueTask<IEnumerable<ExpressViewModel>> GetAllAsync()
    {
        var expresses = await expressRepository.GetAllAsync();
        return expresses.Where(a => !a.IsDeleted).MapTo<ExpressViewModel>();
    }

    public async ValueTask<ExpressViewModel> GetByIdAsync(long id)
    {
        var expresses = await expressRepository.GetAllAsync();
        var existExpress = expresses.FirstOrDefault(a => a.Id == id && !a.IsDeleted)
            ?? throw new Exception($"This express is not found With this id {id}");

        return existExpress.MapTo<ExpressViewModel>();
    }

    public async ValueTask<ExpressViewModel> UpdateAsync(long id, ExpressUpdateModel express, bool isUsesDeleted)
    {
        var existBranch = await branchService.GetByIdAsync(express.BranchId);
        var existTransport = await transportService.GetByIdAsync(express.TransportId);

        var expresses = await expressRepository.GetAllAsync();
        var existExpress = new Express();
        if (isUsesDeleted)
            existExpress = expresses.First(a => a.Id == id);
        else
            existExpress = expresses.FirstOrDefault(a => a.Id == id && !a.IsDeleted)
                ?? throw new Exception($"This express is not found With this id {id}");

        var updatedExpress = await expressRepository.UpdateAsync(id, express.MapTo<Express>());
        return updatedExpress.MapTo<ExpressViewModel>();
    }
}

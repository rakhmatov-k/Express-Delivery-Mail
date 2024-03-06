using ExpressDaliveryMail.Data.IRepositories;
using ExpressDaliveryMail.Data.Repositories;
using ExpressDeliveryMail.Domain.Entities.Branches;
using ExpressDeliveryMail.Domain.Entities.Transports;
using ExpressDeliveryMail.Service.Extensions;
using ExpressDeliveryMail.Service.Interfaces;

namespace ExpressDeliveryMail.Service.Services;

public class TransportService : ITransportService
{
    private TransportRepository transportRepository;
    public TransportService(TransportRepository transportRepository)
    {
        this.transportRepository = transportRepository;
    }

    public async ValueTask<TransportViewModel> CreatedAsync(TransportCreationModel transport)
    {
        var transports = await transportRepository.GetAllAsync();
        var existTransport = transports.FirstOrDefault(t => t.Description == transport.Description && t.Type == transport.Type);
        if (existTransport != null)
        {
            if (existTransport.IsDeleted)
                return await UpdateAsync(existTransport.Id, transport.MapTo<TransportUpdateModel>(), true);
            throw new Exception($"This transport is already exist With this type : {transport.Type} and this description : {transport.Description}");
        }
        var createdTransport = await transportRepository.InsertAsync(transport.MapTo<Transport>());
        return createdTransport.MapTo<TransportViewModel>();
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var transports = await transportRepository.GetAllAsync();
        var existTransport = transports.FirstOrDefault(u => u.Id == id && !u.IsDeleted)
            ?? throw new Exception($"This transport is not found With this id {id}");

        await transportRepository.DeleteAsync(id);
        return true;
    }

    public async ValueTask<IEnumerable<TransportViewModel>> GetAllAsync()
    {
        var transports = await transportRepository.GetAllAsync();
        return transports.MapTo<TransportViewModel>();
    }

    public async ValueTask<TransportViewModel> GetByIdAsync(long id)
    {
        var transports = await transportRepository.GetAllAsync();
        var existTransport = transports.FirstOrDefault(u => u.Id == id && !u.IsDeleted)
            ?? throw new Exception($"This transport is not found With this id {id}");

        return existTransport.MapTo<TransportViewModel>();
    }

    public async ValueTask<TransportViewModel> UpdateAsync(long id, TransportUpdateModel transport, bool isUsusDeleted)
    {
        var transports = await transportRepository.GetAllAsync();
        var existTransport = new Transport();
        if (isUsusDeleted)
            existTransport = transports.First(u => u.Id == id);
        else
            existTransport = transports.FirstOrDefault(u => u.Id == id && !u.IsDeleted)
                ?? throw new Exception($"This transport is not found With this id {id}");

        var updatedTransport = await transportRepository.UpdateAsync(id, transport.MapTo<Transport>());
        return updatedTransport.MapTo<TransportViewModel>();
    }
}

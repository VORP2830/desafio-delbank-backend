using LocadoraDVD.Domain.Entities;
using LocadoraDVD.Domain.Interfaces.Mongo;
using Microsoft.Extensions.Configuration;

namespace LocadoraDVD.Application.Subscribers;
public class DvdMongoSubscribe : SubscribeBase<Dvd>
{
    public DvdMongoSubscribe(IConfiguration configuration, IServiceProvider serviceProvider)
        : base(configuration, serviceProvider, "dvdQueue", "dvd") { }

    protected override async Task CompleteAsync(IUnitOfWorkMongo unitOfWorkMongo, Dvd @event)
    {
        var dvd = await unitOfWorkMongo.DvdMongoRepository.GetById(@event.Id);
        if (dvd == null)
        {
            unitOfWorkMongo.DvdMongoRepository.Add(@event);
        }
        else
        {
            unitOfWorkMongo.DvdMongoRepository.Update(@event);
        }
        await unitOfWorkMongo.SaveChangesAsync();
    }
}
using LocadoraDVD.Domain.Entities;
using LocadoraDVD.Domain.Interfaces.Mongo;
using Microsoft.Extensions.Configuration;

namespace LocadoraDVD.Application.Subscribers;

public class DirectorMongoSubscribe : SubscribeBase<Director>
{
    public DirectorMongoSubscribe(IConfiguration configuration, IServiceProvider serviceProvider)
        : base(configuration, serviceProvider, "directorQueue", "director") { }

    protected override async Task CompleteAsync(IUnitOfWorkMongo unitOfWorkMongo, Director @event)
    {
        var director = await unitOfWorkMongo.DirectorMongoRepository.GetById(@event.Id);
        if (director == null)
        {
            unitOfWorkMongo.DirectorMongoRepository.Add(@event);
        }
        else
        {
            unitOfWorkMongo.DirectorMongoRepository.Update(@event);
        }
        await unitOfWorkMongo.SaveChangesAsync();
    }
}
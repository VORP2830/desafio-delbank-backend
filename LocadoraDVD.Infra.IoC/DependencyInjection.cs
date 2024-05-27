using LocadoraDVD.Application.Dtos.Mapping;
using LocadoraDVD.Application.Interfaces;
using LocadoraDVD.Application.Services;
using LocadoraDVD.Application.Subscribers;
using LocadoraDVD.Domain.Interfaces;
using LocadoraDVD.Domain.Interfaces.Mongo;
using LocadoraDVD.Infra.Data.Context;
using LocadoraDVD.Infra.Data.Messaging;
using LocadoraDVD.Infra.Data.Cache;
using LocadoraDVD.Infra.Data.Repositories;
using LocadoraDVD.Infra.Data.Repositories.MongoRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LocadoraDVD.Infra.IoC;

public static class DependecyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
    {
        var mysqlConnectionString = Environment.GetEnvironmentVariable("MYSQL_URL") ?? configuration.GetConnectionString("MySQLConnection");
        var mongoConnectionString = Environment.GetEnvironmentVariable("MONGO_URL") ?? configuration.GetConnectionString("MongoDBConnection");

        service.AddDbContext<MySQLContext>(options =>
            options.UseMySql(mysqlConnectionString, ServerVersion.AutoDetect(mysqlConnectionString),
                b => b.MigrationsAssembly(typeof(MySQLContext).Assembly.FullName)));

        service.AddDbContext<MongoContext>(options =>
                options.UseMongoDB(mongoConnectionString, "LocadoraDVD"));

        service.AddAutoMapper(typeof(MappingProfile));

        service.AddScoped<IUnitOfWork, UnitOfWork>();
        service.AddScoped<IUnitOfWorkMongo, UnitOfWorkMongo>();
        service.AddScoped<IMessageBusService, MessageBusService>();
        service.AddScoped<ICache, Cache>();

        service.AddScoped<IDvdService, DvdService>();
        service.AddScoped<IDirectorService, DirectorService>();

        service.AddHostedService<DvdMongoSubscribe>();
        service.AddHostedService<DirectorMongoSubscribe>();

        return service;
    }
}
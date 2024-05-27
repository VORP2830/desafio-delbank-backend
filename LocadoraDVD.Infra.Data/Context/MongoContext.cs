using LocadoraDVD.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;

namespace LocadoraDVD.Infra.Data.Context;
public class MongoContext : DbContext
{
    public DbSet<Director> Directors { get; set; }
        public DbSet<Dvd> Dvds { get; set; }
    public static MongoContext Create(IMongoDatabase database)
    {
        var options = new DbContextOptionsBuilder<MongoContext>()
            .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
            .Options;
        return new MongoContext(options);
    }
    public MongoContext(DbContextOptions<MongoContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Dvd>().ToCollection("Dvds");
    }
    private class LocadoraDvdDataBaseSettings()
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string CollectionName { get; set; } = "Dvd";
    }
}


using MongoDB.Driver;
using WebHooks.WebHooksRepository.Services.Data.Models;

namespace WebHooks.WebHooksRepository.Services.Data.Migrations;

public static class InitialMigration
{
    public static void ApplyMigration(IMongoDatabase database, string dbCollectionName)
    {
        var collectionExists = database
            .ListCollectionNames().ToList()
            .Any(name => name == dbCollectionName);

        if (!collectionExists)
        {
            database.CreateCollection(dbCollectionName);
            database.ApplyIndexing(dbCollectionName);
        }
    }

    /// <summary>
    /// Check out https://github.com/pkdone/sharded-mongodb-docker for how to run mongoDb in a sharded fashion.
    /// </summary>
    /// <param name="database"></param>
    /// <param name="dbCollectionName"></param>
    private static void ApplyIndexing(this IMongoDatabase database, string dbCollectionName)
    {
        var collection = database.GetCollection<WebHookDataModel>(dbCollectionName);

        var keys = Builders<WebHookDataModel>
            .IndexKeys
            .Ascending(x => x.EventCode)
            .Ascending(x => x.SourceCode);

        // Create the index with the shard key
        var indexOptions = new CreateIndexOptions {Name = "shard_key_index"};
        var indexModel = new CreateIndexModel<WebHookDataModel>(keys, indexOptions);

        collection.Indexes.CreateOne(indexModel);
    }
}

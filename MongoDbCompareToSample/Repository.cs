using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MongoDbCompareToSample;

public class Repository<TModel, TId>
    where TModel : IIdentity<TId>
    where TId : IComparable<TId>
{
    private readonly IMongoCollection<TModel> _collection;

    public Repository(string connectionString, string databaseName, LinqProvider linqProviderVersion)
    {
        var clientSettings = MongoClientSettings.FromConnectionString(connectionString);
        clientSettings.LinqProvider = linqProviderVersion;
        var mongoClient = new MongoClient(clientSettings);
        var database = mongoClient.GetDatabase(databaseName);

        _collection = database.GetCollection<TModel>(nameof(TModel));
    }

    public async Task<IEnumerable<TModel>> GetAfter(TId cursor)
    {
        return await _collection.AsQueryable()
            .Where(x => x.Id.CompareTo(cursor) > 0)
            .ToListAsync();
    }
}
using MongoDB.Driver.Linq;
using Testcontainers.MongoDb;
using Xunit;

namespace MongoDbCompareToSample.Tests;

public class UnitTest1 : IAsyncLifetime
{
    private readonly MongoDbContainer _mongoDbContainer =
        new MongoDbBuilder().Build();

    [Theory]
    [InlineData(LinqProvider.V2)]
    [InlineData(LinqProvider.V3)]
    public async Task CompareToWorks(LinqProvider linqProvider)
    {
        var repository = new Repository<TestModel, Guid>(
            _mongoDbContainer.GetConnectionString(),
            linqProvider.ToString(),
            linqProvider
        );

        var randomId = Guid.NewGuid();
        await repository.GetAfter(randomId);
    }

    public async Task InitializeAsync()
        => await _mongoDbContainer.StartAsync();

    public async Task DisposeAsync()
        => await _mongoDbContainer.DisposeAsync().AsTask();
}
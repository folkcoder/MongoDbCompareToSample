namespace MongoDbCompareToSample.Tests;

public record TestModel : IIdentity<Guid>
{
    public Guid Id { get; set; }
}

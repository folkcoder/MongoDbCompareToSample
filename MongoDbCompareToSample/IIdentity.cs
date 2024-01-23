namespace MongoDbCompareToSample;

public interface IIdentity<TId>
{
    public TId Id { get; set; }
}

using MongoDB.Driver;

namespace Itau.TestePratico.Infra.Context
{
    public interface IMongoDbContext
    {
        IMongoCollection<T> GetCollection<T>(string collection);
    }
}

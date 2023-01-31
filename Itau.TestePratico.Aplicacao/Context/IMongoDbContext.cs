using MongoDB.Driver;

namespace Itau.TestePratico.Aplicacao.Context
{
    public interface IMongoDbContext
    {
        IMongoCollection<T> GetCollection<T>(string collection);
    }
}

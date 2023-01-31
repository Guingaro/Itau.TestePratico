using Itau.TestePratico.Donino.Modelo;
using MongoDB.Driver;
using System;

namespace Itau.TestePratico.Infra.Context
{
    public class MongoDbContext : IMongoDbContext
    {
        private IMongoDatabase _database { get; }

        public MongoDbContext(string ConnectionString)
        {
            try
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));

                settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
                
                _database = new MongoClient(settings).GetDatabase(ConnectionString);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível se conectar com o servidor.", ex);
            }
        }
        public IMongoCollection<T> GetCollection<T>(string collection)
        {
            return _database.GetCollection<T>(collection);
        }

    }
}

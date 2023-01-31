using MongoDB.Driver;
using System;

namespace Itau.TestePratico.Aplicacao.Context
{
    public class MongoDbContext : IMongoDbContext
    {
        public const string DATABASE_NAME = "Feriado";
        public IMongoDatabase _database { get; }

        public MongoDbContext(string ConnectionString)
        {
            try
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));

                settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };

                var mongoClient = new MongoClient(ConnectionString);

                _database = mongoClient.GetDatabase(DATABASE_NAME);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível se conectar com o servidor.", ex);
            }
        }
        public IMongoCollection<T> GetCollection<T>(string collection)
        {
            if (string.IsNullOrEmpty(collection)) return null;

            return _database.GetCollection<T>(collection);
        }

    }
}

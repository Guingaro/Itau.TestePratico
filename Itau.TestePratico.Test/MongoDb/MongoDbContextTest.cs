using Itau.TestePratico.Aplicacao.Context;
using Itau.TestePratico.Dominio.Modelo;
using MongoDB.Driver;
using Moq;
using System;
using Xunit;

namespace Itau.TestePratico.Test.MongoDb
{
    public class MongoDbContextTest
    {

        private Mock<MongoDbContext> _mockDBContext;

        private Mock<MongoClient> _mockClient;
        public MongoDbContextTest()
        {
            _mockDBContext = new Mock<MongoDbContext>();
            _mockClient = new Mock<MongoClient>();
        }

        [Fact]
        public void WhenMongoDBContextConstructorIsSuccess()
        {
            string Connection = "mongodb://tes123";
            string DataBase = "Feriado";

            //Act 
            var context = new MongoDbContext(Connection);

            //Assert 
            Assert.NotNull(context);
            Assert.Equal(context._database.DatabaseNamespace.DatabaseName, DataBase);
        }
        [Fact]
        public void WhenMongoDBContextGetCollectionIsNameEmpty()
        {
            string Connection = "mongodb://tes123";

            //Act 
            var context = new MongoDbContext(Connection);
            var myCollecton = context.GetCollection<Feriado>("");

            //Assert 
            Assert.Null(myCollecton);
        }

        [Fact]
        public void MongoDBContext_Constructor_NotSuccess()
        {
            string Connection = null;

            Assert.Throws<Exception>(() => new MongoDbContext(Connection));
        }
    }
}

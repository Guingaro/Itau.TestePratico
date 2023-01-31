using Itau.TestePratico.Aplicacao.Context;
using Itau.TestePratico.Aplicacao.Repositorio;
using Itau.TestePratico.Dominio.Enum;
using Itau.TestePratico.Dominio.Modelo;
using Itau.TestePratico.Test.MongoDb.Mock;
using MongoDB.Driver;
using Moq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using Xunit;

namespace Itau.TestePratico.Test.MongoDb
{
    public class RepositorioFeriadoTest
    {
        private readonly Mock<IMongoDbContext> _contextMock;
        private readonly Mock<IMongoCollection<Feriado>> _collectionMock;

        public RepositorioFeriadoTest()
        {
            _contextMock = new Mock<IMongoDbContext>();
            _collectionMock = new Mock<IMongoCollection<Feriado>>();
        }

        [Fact]
        public async void WhenAddNewFeriadoNotShouldException()
        {
            List<Feriado> feriados = new List<Feriado>
            {
                new Feriado
                {
                    Data = "30/01/2023",
                    Nome = "Teste",
                    Tipo = TipoFeriado.Nacional,
                }
            };

            var _feriadoCursor = new Mock<IAsyncCursor<Feriado>>();
            _feriadoCursor.Setup(x => x.Current).Returns(feriados);
            _feriadoCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);

            _collectionMock.Setup(x => x.FindAsync(It.IsAny<FilterDefinition<Feriado>>(), It.IsAny<FindOptions<Feriado, Feriado>>(), It.IsAny<CancellationToken>())).ReturnsAsync(_feriadoCursor.Object);
            _contextMock.Setup(x => x.GetCollection<Feriado>(typeof(Feriado).Name)).Returns(_collectionMock.Object);

            _contextMock.Setup(c => c.GetCollection<Feriado>(It.IsAny<string>())).Returns(_collectionMock.Object);
            var feriado = FeriadoMock.GetFeriadoMock();
            var repositorio = new RepositorioFeriado(_contextMock.Object);

            //act
            await repositorio.Criar(feriado);

            //ass
            _collectionMock.Verify(x => x.InsertOneAsync(It.IsAny<Feriado>(), It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()), Times.Once());
        }        



        [Theory]
        [InlineData("30/03/2023")]
        [InlineData("01/01/2023")]
        [InlineData("25/12/2023")]
        public void TestingDateFormatsShouldBeTrue(string Data)
        {
            var rc = new Regex(@"(0[1-9]|[12][0-9]|3[01])/([01][0-9])/([0-9]{4})", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            var act = rc.IsMatch(Data);

            Assert.True(act);
        }

        [Theory]
        [InlineData("2023-03-03")]
        [InlineData("a/01/2023")]
        [InlineData("25/12/202n")]
        public void TestingDateFormatsShouldBeFalse(string Data)
        {
            var rc = new Regex(@"(0[1-9]|[12][0-9]|3[01])/([01][0-9])/([0-9]{4})", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            var act = rc.IsMatch(Data);

            Assert.False(act);
        }
    }
}

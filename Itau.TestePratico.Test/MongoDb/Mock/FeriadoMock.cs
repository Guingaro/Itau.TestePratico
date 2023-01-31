using FizzWare.NBuilder;
using Itau.TestePratico.Dominio.Modelo;
using Itau.TestePratico.Dominio.Enum;
using System;

namespace Itau.TestePratico.Test.MongoDb.Mock
{
    public static class FeriadoMock
    {
        public static Feriado GetFeriadoMock()
            => Builder<Feriado>
                .CreateNew()
                    .With(i => i.Tipo, TipoFeriado.Municipal)
                    .With(i => i.Data, new DateTime().Date.ToString())
                    .With(i => i.Nome, "Feriado Teste")
                    .Build();

        public static Feriado GetFeriadoDateIsBrokenMock(string Date)
            => Builder<Feriado>
                .CreateNew()
                    .With(i => i.Tipo, TipoFeriado.Municipal)
                    .With(i => i.Data, Date)
                    .With(i => i.Nome, "Feriado Teste")
                    .Build();
    }
}

using Itau.TestePratico.Dominio.Enum;
using Itau.TestePratico.Dominio.Modelo.Abstract;

namespace Itau.TestePratico.Dominio.Modelo
{

    public class Feriado : Entity
    {
        public string Data { get; set; }
        public string Nome { get; set; }
        public TipoFeriado Tipo { get; set; }
    }
}

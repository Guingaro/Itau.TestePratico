using Itau.TestePratico.Dominio.Enum;
using Itau.TestePratico.Dominio.Modelo.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Itau.TestePratico.Dominio.Modelo
{

    public class Feriado : Entity
    {
        [Required]
        public string Data { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public TipoFeriado Tipo { get; set; }
    }
}

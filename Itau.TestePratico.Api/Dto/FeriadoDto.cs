using Itau.TestePratico.Dominio.Enum;
using System.ComponentModel.DataAnnotations;

namespace Itau.TestePratico.Api.Dto
{
    public class FeriadoDto
    {
        [Required]
        public string Data { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public TipoFeriado Tipo { get; set; }
    }
}

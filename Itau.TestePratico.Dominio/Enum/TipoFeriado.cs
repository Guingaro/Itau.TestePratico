using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Itau.TestePratico.Dominio.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TipoFeriado
    {
        [Description("Municipal")]
        Municipal,
        [Description("Estadual")]
        Estadual,
        [Description("Nacional")]
        Nacional
    }
}

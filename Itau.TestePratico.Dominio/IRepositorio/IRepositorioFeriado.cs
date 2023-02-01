using Itau.TestePratico.Dominio.Enum;
using Itau.TestePratico.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Itau.TestePratico.Dominio.IRepositorio
{
    public interface IRepositorioFeriado
    {
        Task<List<Feriado>> Obter();
        Task<List<Feriado>> ObterPorMesAno(string Mes, string Ano);
        Task Criar(Feriado feriado);
        Task Atualizar(Guid Id, Feriado feriado);
        Task AtualizarNome(Guid Id, string Nome);
        Task AtualizarData(Guid Id, string data);
        Task AtualizarTipo(Guid Id, TipoFeriado tipoFeriado);
        Task Remover(Guid Id);
    }
}

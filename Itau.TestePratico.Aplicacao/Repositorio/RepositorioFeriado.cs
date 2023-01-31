using Itau.TestePratico.Aplicacao.Context;
using Itau.TestePratico.Aplicacao.Context.Base;
using Itau.TestePratico.Dominio.IRepositorio;
using Itau.TestePratico.Dominio.Modelo;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Itau.TestePratico.Aplicacao.Repositorio
{
    public class RepositorioFeriado : RepositorioBase<Feriado>, IRepositorioFeriado
    {
        Regex rxDate = null;
        public RepositorioFeriado(IMongoDbContext context) : base(context)
        {
            rxDate = new Regex(@"(0[1-9]|[12][0-9]|3[01])/([01][0-9])/([0-9]{4})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public async Task<List<Feriado>> Obter()
            => await _dbSet.FindSync(m => true, new FindOptions<Feriado, Feriado>() { Sort = SortDefinition() }).ToListAsync();

        public async Task<List<Feriado>> ObterPorMesAno(string Mes, string Ano)
            => await _dbSet.FindSync(Builders<Feriado>.Filter.Regex("Data", new BsonRegularExpression($".*{Mes}/{Ano}.*"))).ToListAsync(); 
       

        public override async Task Atualizar(Guid Id, Feriado entity)
        {
            var feriado = await _dbSet.FindSync(Filter.Eq("Data", entity.Data)).FirstOrDefaultAsync();

            if (feriado != null && !(feriado.Data == entity.Data)) throw new Exception(string.Format("{0},{1}", "Já existe feriado para data informada.", $"Feriado : {feriado.ToJson()}"));

            if (entity.Data != null && !rxDate.IsMatch(entity.Data)) throw new Exception($"O Formato esperado para data é dd/mm/aaaa. Ex: {DateTime.UtcNow.ToString("dd/MM/yyyy")}");

            entity.Id = Id;

            await base.Atualizar(Id, entity);
        }

        public override async Task Criar(Feriado entity)
        {
            var feriado = await _dbSet.FindSync(Filter.Eq("Data", entity.Data)).FirstOrDefaultAsync();

            if (feriado != null) throw new Exception(string.Format("{0},{1}", "Já existe feriado para data informada.", $"Feriado : {feriado.ToJson()}"));

            if (entity.Data != null && !rxDate.IsMatch(entity.Data)) throw new Exception($"O Formato esperado para data é dd/mm/aaaa. Ex: {DateTime.UtcNow.ToString("dd/MM/yyyy")}");

            await base.Criar(entity);
        }

        public override async Task Remover(Guid Id)
        {
            var feriado = await _dbSet.FindSync(Filter.Eq("_id", Id)).FirstOrDefaultAsync();

            if (feriado == null) throw new Exception("Não existe feriado para o Id informado.");

            await base.Remover(Id);
        }

        private SortDefinition<Feriado> SortDefinition() => Builders<Feriado>.Sort.Descending(x => x.Data);
    }
}

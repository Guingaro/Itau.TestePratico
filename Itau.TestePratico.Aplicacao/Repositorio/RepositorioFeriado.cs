using Itau.TestePratico.Aplicacao.Context;
using Itau.TestePratico.Aplicacao.Context.Base;
using Itau.TestePratico.Dominio.Enum;
using Itau.TestePratico.Dominio.IRepositorio;
using Itau.TestePratico.Dominio.Modelo;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Itau.TestePratico.Aplicacao.Repositorio
{
    public class RepositorioFeriado : RepositorioBase<Feriado>, IRepositorioFeriado
    {
       
        public RepositorioFeriado(IMongoDbContext context) : base(context)
        {
            
        }

        public async Task<List<Feriado>> Obter()
            => await _dbSet.FindSync(m => true, new FindOptions<Feriado, Feriado>() { Sort = SortDefinition() }).ToListAsync();

        public async Task<List<Feriado>> ObterPorMesAno(string Mes, string Ano)
            => await _dbSet.FindSync(Builders<Feriado>.Filter.Regex("Data", new BsonRegularExpression($".*{Mes}/{Ano}.*"))).ToListAsync();

        public override async Task Atualizar(Guid Id, Feriado entity)
        {
            var feriado = await _dbSet.FindSync(Filter.Eq("_id", Id)).FirstOrDefaultAsync();

            if (feriado == null) throw new Exception("Id de Feriado Inexistente.");

            if (!await ValidacaoData(entity.Data)) throw new Exception($"O Formato esperado para data é dd/mm/aaaa. Ex: {DateTime.UtcNow.ToString("dd/MM/yyyy")}");

            if (await VerificaDuplicidadeData(entity.Data)) throw new Exception("Já existe feriado para data informada.");

            entity.Id = Id;

            await base.Atualizar(Id, entity);
        }
        public async Task AtualizarNome(Guid Id, string nome)
        {
            var feriado = await _dbSet.FindSync(Filter.Eq("_id", Id)).FirstOrDefaultAsync();

            if (feriado == null) throw new Exception("Id de Feriado Inexistente.");

            await base.Atualizar(Id, new Feriado
            {
                Id = Id,
                Data = feriado.Data,
                Nome = nome,
                Tipo = feriado.Tipo
            });
        }
        public async Task AtualizarData(Guid Id, string data)
        {
            var feriado = await _dbSet.FindSync(Filter.Eq("_id", Id)).FirstOrDefaultAsync();

            if (feriado == null) throw new Exception("Id de Feriado Inexistente.");

            if (!await ValidacaoData(data)) throw new Exception($"O Formato esperado para data é dd/mm/aaaa. Ex: {DateTime.UtcNow.ToString("dd/MM/yyyy")}");

            if (await VerificaDuplicidadeData(data)) throw new Exception("Já existe feriado para data informada.");

            await base.Atualizar(Id, new Feriado
            {
                Id = Id,
                Data = data,
                Nome = feriado.Nome,
                Tipo = feriado.Tipo
            });
        }
        public async Task AtualizarTipo(Guid Id, TipoFeriado tipoFeriado)
        {
            var feriado = await _dbSet.FindSync(Filter.Eq("_id", Id)).FirstOrDefaultAsync();

            if (feriado == null) throw new Exception("Id de Feriado Inexistente.");

            await base.Atualizar(Id, new Feriado
            {
                Id = Id,
                Data = feriado.Data,
                Nome = feriado.Nome,
                Tipo = tipoFeriado
            });
        }
        public override async Task Criar(Feriado entity)
        {
            if (await VerificaDuplicidadeData(entity.Data)) throw new Exception("Já existe feriado para data informada.");

            if (!await ValidacaoData(entity.Data)) throw new Exception($"O Formato esperado para data é dd/mm/aaaa. Ex: {DateTime.UtcNow.ToString("dd/MM/yyyy")}");

            await base.Criar(entity);
        }
        public override async Task Remover(Guid Id)
        {
            var feriado = await _dbSet.FindSync(Filter.Eq("_id", Id)).FirstOrDefaultAsync();

            if (feriado == null) throw new Exception("Não existe feriado para o Id informado.");

            await base.Remover(Id);
        }
        async Task<bool> VerificaDuplicidadeData(string data)
                => await _dbSet.FindSync(Filter.Eq("Data", data)).AnyAsync();
        async Task<bool> ValidacaoData(string data)
        {
            if (!DateTime.TryParse(data, new CultureInfo("pt-BR"), DateTimeStyles.None, out var date))
                return await Task.FromResult(false);

            Regex rxDate = new Regex(@"(0[1-9]|[12][0-9]|3[01])/([01][0-9])/([0-9]{4})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            if (!rxDate.IsMatch(data))
                return await Task.FromResult(false);

            return await Task.FromResult(true);

        }
        private SortDefinition<Feriado> SortDefinition() => Builders<Feriado>.Sort.Descending(x => x.Data);
    }
}

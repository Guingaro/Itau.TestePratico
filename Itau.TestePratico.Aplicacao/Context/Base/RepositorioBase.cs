using Itau.TestePratico.Dominio.Modelo.Abstract;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Itau.TestePratico.Aplicacao.Context.Base
{
    public abstract class RepositorioBase<TEntity> where TEntity : Entity
    {
        protected readonly IMongoDbContext _context;
        protected readonly IMongoCollection<TEntity> _dbSet;

        public FilterDefinitionBuilder<TEntity> Filter => Builders<TEntity>.Filter;
        protected RepositorioBase(IMongoDbContext context)
        {
            _context = context;
            _dbSet = _context.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public virtual async Task Criar(TEntity entity)
            => await _dbSet.InsertOneAsync(entity);
        

        public virtual async Task Atualizar(Guid Id, TEntity entity)
            => await _dbSet.ReplaceOneAsync(Filter.Eq("_id", Id), entity);
       

        public virtual async Task Remover(Guid Id)
            => await _dbSet.DeleteOneAsync(Filter.Eq("_id", Id));    
        

    }
}

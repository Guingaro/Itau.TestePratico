using Itau.TestePratico.Donino.Modelo.Abstract;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Itau.TestePratico.Infra.Context.Base
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
        {
            await _dbSet.InsertOneAsync(entity);
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            await _dbSet.ReplaceOneAsync(Filter.Eq("_Id", entity.Id), entity);
        }

        public virtual async Task Remover(TEntity entity)
        {
            await _dbSet.DeleteOneAsync(Filter.Eq("_Id", entity.Id));
        }        
        

    }
}

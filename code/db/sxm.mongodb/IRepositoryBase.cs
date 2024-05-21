using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Sxm.MongoDB;

public interface IRepositoryBase
{
    string CollectionName { get; }
}

public interface IRepositoryBase<TEntity> : IRepositoryBase
{
    IReadOnlyList<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
    Task<IReadOnlyList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);
    TEntity? Get(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate);
    bool UpdateOne(Expression<Func<TEntity, bool>> expression, UpdateDefinition<TEntity> definition);

    Task<bool> UpdateOneAsync(Expression<Func<TEntity, bool>> expression,
        UpdateDefinition<TEntity> definition);

    bool ReplaceOne<TField>(Expression<Func<TEntity, TField>> field, TField value, TEntity newEntity);
    Task<bool> ReplaceOneAsync<TField>(Expression<Func<TEntity, TField>> field, TField value, TEntity newEntity);
    bool Add(TEntity entity);
    bool Add(params TEntity[] entities);
    Task<bool> AddAsync(TEntity entity);
    Task<bool> AddAsync(params TEntity[] entities);
    bool DeleteOne(Expression<Func<TEntity, bool>> condition);
    Task<bool> DeleteOneAsync(Expression<Func<TEntity, bool>> condition);
    Task<bool> DeleteManyAsync(Expression<Func<TEntity, bool>> condition);
    bool Exists(Expression<Func<TEntity, bool>> predicate);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    bool Clean();
    Task<bool> CleanAsync();
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CitizenFX.Core;
using MongoDB.Driver;

namespace Sxm.MongoDB;

public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
    private readonly IContextFactory _db;

    public abstract string CollectionName { get; }

    protected FilterDefinitionBuilder<TEntity> Filter => Builders<TEntity>.Filter;
    protected UpdateDefinitionBuilder<TEntity> Update => Builders<TEntity>.Update;

    protected RepositoryBase(IContextFactory db)
    {
        _db = db;
    }

    protected IMongoCollection<TEntity> GetCollection()
    {
        return _db.Database.GetCollection<TEntity>(CollectionName);
    }

    public virtual IReadOnlyList<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
    {
        return GetCollection().Find(predicate).ToList();
    }

    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return (await GetCollection().FindAsync<TEntity>(predicate)).ToList();
    }

    public virtual TEntity? Get(Expression<Func<TEntity, bool>> predicate)
    {
        return GetCollection().Find(predicate).FirstOrDefault();
    }

    public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return (await GetCollection().FindAsync(predicate)).FirstOrDefault();
    }

    public virtual bool UpdateOne(Expression<Func<TEntity, bool>> expression,
        UpdateDefinition<TEntity> definition)
    {
        try
        {
            GetCollection().UpdateOne(expression, definition);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public virtual async Task<bool> UpdateOneAsync(Expression<Func<TEntity, bool>> expression,
        UpdateDefinition<TEntity> definition)
    {
        try
        {
            await GetCollection().UpdateOneAsync(expression, definition);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public virtual bool ReplaceOne<TField>(Expression<Func<TEntity, TField>> field, TField value, TEntity newEntity)
    {
        return GetCollection().ReplaceOne(Filter.Eq(field, value), newEntity).ModifiedCount > 0;
    }

    public virtual async Task<bool> ReplaceOneAsync<TField>(Expression<Func<TEntity, TField>> field, TField value,
        TEntity newEntity)
    {
        return (await GetCollection().ReplaceOneAsync(Filter.Eq(field, value), newEntity)).ModifiedCount > 0;
    }

    public virtual bool Add(TEntity entity)
    {
        try
        {
            GetCollection().InsertOne(entity);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{nameof(Add)} -> {ex.StackTrace}");
            return false;
        }
    }

    public bool Add(params TEntity[] entities)
    {
        try
        {
            GetCollection().InsertMany(entities);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{nameof(Add)} -> {ex.Message}");
            return false;
        }
    }

    public virtual async Task<bool> AddAsync(TEntity entity)
    {
        try
        {
            await GetCollection().InsertOneAsync(entity);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{nameof(AddAsync)} -> {ex.Message}");
            return false;
        }
    }

    public async Task<bool> AddAsync(params TEntity[] entities)
    {
        try
        {
            await GetCollection().InsertManyAsync(entities);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{nameof(AddAsync)} -> {ex.Message}");
            return false;
        }
    }

    public virtual bool DeleteOne(Expression<Func<TEntity, bool>> condition)
    {
        return GetCollection().DeleteOne(condition).DeletedCount > 0;
    }

    public virtual async Task<bool> DeleteOneAsync(Expression<Func<TEntity, bool>> condition)
    {
        return (await GetCollection().DeleteOneAsync(condition)).DeletedCount > 0;
    }

    public virtual async Task<bool> DeleteManyAsync(Expression<Func<TEntity, bool>> condition)
    {
        return (await GetCollection().DeleteManyAsync(condition)).DeletedCount > 0;
    }

    public virtual bool Exists(Expression<Func<TEntity, bool>> predicate)
    {
        return Get(predicate) is not null;
    }

    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await GetAsync(predicate) is not null;
    }

    public bool Clean()
    {
        try
        {
            GetCollection().DeleteMany(FilterDefinition<TEntity>.Empty);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> CleanAsync()
    {
        try
        {
            await GetCollection().DeleteManyAsync(FilterDefinition<TEntity>.Empty);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
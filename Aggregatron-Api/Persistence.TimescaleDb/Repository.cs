using Domain.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Persistence.TimescaleDb;

public abstract class Repository<T> : IRepository where T : class, IEntity
{
    protected readonly DbSet<T> DbSet;
    protected AggregatronPersistenceContext Persistence { get; }

    public Repository(AggregatronPersistenceContext persistence, DbSet<T> dbSet)
    {
        DbSet = dbSet;
        Persistence = persistence;
    }
    
    public IPersistenceContext PersistenceContext => Persistence;
    
    public void Delete(int id)
    {
        var entity = DbSet.Find(id);
        if (entity != null)
        {
            DbSet.Remove(entity);
        }
    }
}
using Apps.Settings;

namespace Domain.Common.Persistence;

public interface IPersistenceContext
{
    public AggregatronConfiguration Configuration { get; }
    
    public int SaveChanges();
    public Task<int> SaveChangesAsync();
}
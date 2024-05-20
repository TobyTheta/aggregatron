namespace Domain.Common.Persistence;

public interface IRepository
{
    public IPersistenceContext PersistenceContext { get; }
}
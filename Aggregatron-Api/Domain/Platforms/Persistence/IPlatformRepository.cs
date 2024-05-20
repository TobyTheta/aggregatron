using Domain.Common.Persistence;
using Domain.Common.Services;

namespace Domain.Platforms.Persistence;

public interface IPlatformRepository : IRepository
{
    Task<Response<Platform>> CreateOrUpdate(Platform platform);
}
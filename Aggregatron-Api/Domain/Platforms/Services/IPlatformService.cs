using Domain.Common.Services;

namespace Domain.Platforms.Services;

public interface IPlatformService
{
    Task<Response<Platform>> Create(Platform platform);
}
using HotChocolate.Resolvers;
using CloudIn.Core.ApplicationDomain.Contracts.Repositories;
using CloudIn.Core.ApplicationDomain.Entities;

namespace CloudIn.Core.WebApi.GraphQl.Schema.Resolvers;

public class FileResolver
{
    public IEnumerable<UserEntity> GetOwnerUser(
        IResolverContext context,
        [Service] IUserRepository userRepository
    )
    {
        var parent = context.Parent<FileEntity>();

        return userRepository.GetAll().AsQueryable().Where(user => user.Id == parent.OwnerUserId);
    }

    public string? GetMediaUrl(
        IResolverContext context,
        [Service] IHttpContextAccessor httpContextAccessor,
        [Service] LinkGenerator linker
    )
    {
        var parent = context.Parent<FileEntity>();

        return linker.GetUriByName(
            httpContext: httpContextAccessor?.HttpContext ?? default!,
            endpointName: "download",
            values: new { FileId = parent.Id }
        );
    }
}

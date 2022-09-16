using HotChocolate.Resolvers;
using CloudIn.Core.ApplicationDomain.Contracts.Repositories;
using CloudIn.Core.ApplicationDomain.Entities;

namespace CloudIn.Core.WebApi.GraphQl.Schema.Resolvers;

public class FolderResolver
{
    public IEnumerable<UserEntity> GetOwnerUser(
        IResolverContext context,
        [Service] IUserRepository userRepository
    )
    {
        var parent = context.Parent<FolderEntity>();

        return userRepository.GetAll().AsQueryable().Where(user => user.Id == parent.OwnerUserId);
    }
}

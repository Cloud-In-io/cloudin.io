using HotChocolate.Resolvers;
using CloudIn.Core.Domain.Contracts.Repositories;
using CloudIn.Core.Domain.Entities;

namespace CloudIn.Core.WebApi.GraphQl.Schema.Resolvers;

public class UserResolver
{
    public IEnumerable<FolderEntity> GetRootFolder(
        IResolverContext context,
        [Service] IFolderRepository folderRepository
    )
    {
        var parent = context.Parent<UserEntity>();

        return folderRepository
            .GetAll()
            .AsQueryable()
            .Where(folder => folder.Id == parent.RootFolderId);
    }
}

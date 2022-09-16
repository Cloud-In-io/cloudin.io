using CloudIn.Core.ApplicationDomain.Entities;
using CloudIn.Core.WebApi.GraphQl.Schema.Resolvers;

namespace CloudIn.Core.WebApi.GraphQl.Schema.EntityTypes;

public class FolderType : ObjectType<FolderEntity>
{
    protected override void Configure(IObjectTypeDescriptor<FolderEntity> typeDesc)
    {
        typeDesc.Name("Folder");
        typeDesc.Field(folder => folder.OwnerUserId).IsProjected();

        typeDesc
            .Field("OwnerUser")
            .UseFirstOrDefault()
            .UseProjection()
            .ResolveWith<FolderResolver>(res => res.GetOwnerUser(default!, default!));
    }
}

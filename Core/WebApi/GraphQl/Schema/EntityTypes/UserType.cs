using CloudIn.Core.ApplicationDomain.Entities;
using CloudIn.Core.WebApi.GraphQl.Schema.Resolvers;

namespace CloudIn.Core.WebApi.GraphQl.Schema.EntityTypes;

public class UserType : ObjectType<UserEntity>
{
    protected override void Configure(IObjectTypeDescriptor<UserEntity> typeDesc)
    {
        typeDesc.Name("User");
        typeDesc.Ignore(user => user.Password);
        typeDesc.Field(user => user.RootFolderId).IsProjected();

        typeDesc
            .Field("RootFolder")
            .UseFirstOrDefault()
            .UseProjection()
            .ResolveWith<UserResolver>(res => res.GetRootFolder(default!, default!));
    }
}

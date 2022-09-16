using CloudIn.Core.ApplicationDomain.Entities;

namespace CloudIn.Core.WebApi.GraphQl.Schema.EntityTypes;

public class UserType : ObjectType<UserEntity>
{
    protected override void Configure(IObjectTypeDescriptor<UserEntity> typeDesc)
    {
        typeDesc.Name("User");
        typeDesc.Ignore(user => user.Password);
        typeDesc.Field(user => user.RootFolderId).IsProjected();
    }
}

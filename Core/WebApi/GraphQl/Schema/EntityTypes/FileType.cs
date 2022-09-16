using CloudIn.Core.ApplicationDomain.Entities;
using CloudIn.Core.WebApi.GraphQl.Schema.Resolvers;

namespace CloudIn.Core.WebApi.GraphQl.Schema.EntityTypes;

public class FileType : ObjectType<FileEntity>
{
    protected override void Configure(IObjectTypeDescriptor<FileEntity> typeDesc)
    {
        typeDesc.Name("File");
        typeDesc.Field(folder => folder.PhysicalPath).Ignore();
        typeDesc.Field(folder => folder.OwnerUserId).IsProjected();

        typeDesc
            .Field("OwnerUser")
            .UseFirstOrDefault()
            .UseProjection()
            .ResolveWith<FileResolver>(res => res.GetOwnerUser(default!, default!));
    }
}

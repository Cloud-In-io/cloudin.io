using CloudIn.Core.Domain.Entities;
using CloudIn.Core.WebApi.GraphQl.Schema.Resolvers;

namespace CloudIn.Core.WebApi.GraphQl.Schema.EntityTypes;

public class FileType : ObjectType<FileEntity>
{
    protected override void Configure(IObjectTypeDescriptor<FileEntity> typeDesc)
    {
        typeDesc.Name("File");
        typeDesc.Field(file => file.FilePath).Ignore();
        typeDesc.Field(file => file.OwnerUserId).IsProjected();

        typeDesc
            .Field("Url")
            .ResolveWith<FileResolver>(res => res.GetMediaUrl(default!, default!, default!));

        typeDesc
            .Field("OwnerUser")
            .UseFirstOrDefault()
            .UseProjection()
            .ResolveWith<FileResolver>(res => res.GetOwnerUser(default!, default!));
    }
}

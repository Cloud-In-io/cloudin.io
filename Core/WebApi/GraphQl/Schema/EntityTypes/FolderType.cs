using CloudIn.Core.ApplicationDomain.Entities;

namespace CloudIn.Core.WebApi.GraphQl.Schema.EntityTypes;

public class FolderType : ObjectType<FolderEntity>
{
    protected override void Configure(IObjectTypeDescriptor<FolderEntity> typeDesc)
    {
        typeDesc.Name("Folder");
    }
}

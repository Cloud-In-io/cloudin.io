using CloudIn.Core.ApplicationDomain.Entities;

namespace CloudIn.Core.WebApi.GraphQl.Schema.EntityTypes;

public class FileType : ObjectType<FileEntity>
{
    protected override void Configure(IObjectTypeDescriptor<FileEntity> typeDesc)
    {
        typeDesc.Name("File");
        typeDesc.Field(folder => folder.PhysicalPath).Ignore();
    }
}

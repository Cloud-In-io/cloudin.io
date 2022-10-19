using CloudIn.Core.ApplicationDomain.Entities;
using HotChocolate.Data.Filters;

namespace CloudIn.Core.WebApi.GraphQl.Schema.FilterTypes;

public class UserFilterType : FilterInputType<UserEntity>
{
    protected override void Configure(IFilterInputTypeDescriptor<UserEntity> descriptor)
    {
        descriptor.Name("UserFilterInput");
        descriptor.BindFieldsImplicitly();
        descriptor.Ignore(f => f.Password);
    }
}

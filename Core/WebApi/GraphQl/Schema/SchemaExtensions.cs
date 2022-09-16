using HotChocolate.Execution.Configuration;
using CloudIn.Core.WebApi.GraphQl.Schema.Queries;

namespace CloudIn.Core.WebApi.GraphQl.Schema;

public static class SchemaExtensions
{
    public static IRequestExecutorBuilder AddQueries(this IRequestExecutorBuilder builder) =>
        builder
            .AddQueryType<BaseQuery>()
            .AddTypeExtension<UserQuery>()
            .AddTypeExtension<FileQuery>()
            .AddTypeExtension<FolderQuery>();
}

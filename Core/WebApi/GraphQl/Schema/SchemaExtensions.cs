using HotChocolate.Execution.Configuration;
using CloudIn.Core.WebApi.GraphQl.Schema.Queries;
using CloudIn.Core.WebApi.GraphQl.Schema.Mutations;
using CloudIn.Core.WebApi.GraphQl.Schema.EntityTypes;

namespace CloudIn.Core.WebApi.GraphQl.Schema;

public static class SchemaExtensions
{
    public static IRequestExecutorBuilder AddQueries(this IRequestExecutorBuilder builder) =>
        builder
            .AddQueryType<BaseQuery>()
            .AddTypeExtension<UserQuery>()
            .AddTypeExtension<FileQuery>()
            .AddTypeExtension<FolderQuery>();

    public static IRequestExecutorBuilder AddMutations(this IRequestExecutorBuilder builder) =>
        builder
            .AddMutationType<BaseMutation>()
            .AddTypeExtension<AuthMutation>()
            .AddTypeExtension<UserMutation>()
            .AddTypeExtension<FileMutation>()
            .AddTypeExtension<FolderMutation>();

    public static IRequestExecutorBuilder AddTypes(this IRequestExecutorBuilder builder) =>
        builder
        .AddType<UserType>()
        .AddType<FileType>()
        .AddType<FolderType>();
}

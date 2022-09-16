using Microsoft.EntityFrameworkCore;
using CloudIn.Core.Data;
using CloudIn.Core.Data.Repositories;
using CloudIn.Core.ApplicationDomain.Contracts.Repositories;
using CloudIn.Core.ApplicationDomain.Services.UserService;
using CloudIn.Core.ApplicationDomain.Services.UserService.Interfaces;
using CloudIn.Core.WebApi.Common.Extensions;
using CloudIn.Core.WebApi.GraphQl.Schema.Queries;
using CloudIn.Core.WebApi.GraphQl.Schema.EntityTypes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(
    opt =>
        opt.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly("WebApi")
        )
);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services
    .AddScoped<IUserService, UserService>()
    .AddScoped<IUserRepository, UserRepository>()
    .AddScoped<IFileRepository, FileRepository>()
    .AddScoped<IFolderRepository, FolderRepository>();

builder.Services
    .AddGraphQLServer()
    .RegisterDbContext<DataContext>()
    .AddType<UserType>()
    .AddType<FileType>()
    .AddType<FolderType>()
    .AddQueryType<UserQuery>()
    .AddProjections();

var app = builder.Build();

await app.ApplyMigrationsAsync<DataContext>();

app.MapGraphQL();

app.Run();

using Microsoft.EntityFrameworkCore;
using CloudIn.Core.Data;
using CloudIn.Core.Data.Repositories;
using CloudIn.Core.ApplicationDomain.Contracts.Repositories;
using CloudIn.Core.ApplicationDomain.Services.UserService;
using CloudIn.Core.ApplicationDomain.Services.UserService.Interfaces;
using CloudIn.Core.WebApi.Common.Extensions;
using CloudIn.Core.WebApi.GraphQl.Schema;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPooledDbContextFactory<DataContext>(
    opt =>
        opt.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly("WebApi")
        )
);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services
    .AddScoped<IUserService, UserService>()
    .AddWithPooledDbContext<IUserRepository, UserRepository>()
    .AddWithPooledDbContext<IFileRepository, FileRepository>()
    .AddWithPooledDbContext<IFolderRepository, FolderRepository>();

builder.Services
    .AddGraphQLServer()
    .RegisterDbContext<DataContext>(DbContextKind.Pooled)
    .AddTypes()
    .AddQueries()
    .AddProjections();

var app = builder.Build();

await app.ApplyMigrationsAsync<DataContext>();

app.MapGraphQL();

app.Run();

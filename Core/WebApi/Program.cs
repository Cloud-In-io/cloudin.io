using Microsoft.EntityFrameworkCore;
using CloudIn.Core.Data;
using CloudIn.Core.Data.Repositories;
using CloudIn.Core.ApplicationDomain.Contracts.Repositories;
using CloudIn.Core.ApplicationDomain.Services.UserService;
using CloudIn.Core.ApplicationDomain.Services.UserService.Interfaces;
using CloudIn.Core.ApplicationDomain.Services.FolderService;
using CloudIn.Core.ApplicationDomain.Services.FolderService.Interfaces;
using CloudIn.Core.WebApi.Common.Extensions;
using CloudIn.Core.WebApi.GraphQl.Schema;
using CloudIn.Core.WebApi.Common.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHttpContextAccessor()
    .AddPooledDbContextFactory<DataContext>(
        opt =>
            opt.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("WebApi")
            )
    );

// configure strongly typed settings object
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("Settings"));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services
    .AddScoped<IUserService, UserService>()
    .AddScoped<IFolderService, FolderService>()
    .AddWithPooledDbContext<IUserRepository, UserRepository>()
    .AddWithPooledDbContext<IFileRepository, FileRepository>()
    .AddWithPooledDbContext<IFolderRepository, FolderRepository>();

builder.Services
    .AddGraphQLServer()
    .RegisterDbContext<DataContext>(DbContextKind.Pooled)
    .AddTypes()
    .AddQueries()
    .AddMutations()
    .AddSorting()
    .AddFiltering()
    .AddProjections()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = builder.Environment.IsDevelopment());

var app = builder.Build();

await app.ApplyMigrationsAsync<DataContext>();

app.MapGraphQL();

app.Run();

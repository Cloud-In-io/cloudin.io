using Microsoft.EntityFrameworkCore;
using CloudIn.Core.Data;
using CloudIn.Core.Data.Repositories;
using CloudIn.Core.Infrastructure.Providers;
using CloudIn.Core.ApplicationDomain.Contracts.Repositories;
using CloudIn.Core.ApplicationDomain.Contracts.Providers.FileSystemProvider;
using CloudIn.Core.ApplicationDomain.Services.UserService;
using CloudIn.Core.ApplicationDomain.Services.UserService.Interfaces;
using CloudIn.Core.ApplicationDomain.Services.FileService;
using CloudIn.Core.ApplicationDomain.Services.FileService.Interfaces;
using CloudIn.Core.ApplicationDomain.Services.FolderService;
using CloudIn.Core.ApplicationDomain.Services.FolderService.Interfaces;
using CloudIn.Core.WebApi.Common.Extensions;
using CloudIn.Core.WebApi.Common.Settings;
using CloudIn.Core.WebApi.GraphQl.Schema;
using CloudIn.Core.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHttpContextAccessor()    
    .Configure<AppSettings>(builder.Configuration.GetSection("Settings"))
    .AddPooledDbContextFactory<DataContext>(
        opt =>
            opt.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("WebApi")
            )
    );

builder.Services
    .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
    .AddScoped<IUserService, UserService>()
    .AddScoped<IFileService, FileService>()
    .AddScoped<IFolderService, FolderService>()
    .AddScoped<IFileSystemProvider, FileSystemProvider>(_ => new FileSystemProvider(@"./public/uploads"))
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
app.MapUploadEndpoint();
app.MapDownloadEndpoint().WithName("download");

app.Run();

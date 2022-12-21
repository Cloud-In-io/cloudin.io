using Microsoft.EntityFrameworkCore;
using tusdotnet.Helpers;
using CloudIn.Core.Data;
using CloudIn.Core.Data.Repositories;
using CloudIn.Core.Domain.Contracts.Repositories;
using CloudIn.Core.Domain.Services.UserService;
using CloudIn.Core.Domain.Services.UserService.Interfaces;
using CloudIn.Core.Domain.Services.FileService;
using CloudIn.Core.Domain.Services.FileService.Interfaces;
using CloudIn.Core.Domain.Services.FolderService;
using CloudIn.Core.Domain.Services.FolderService.Interfaces;
using CloudIn.Core.WebApi.Common.Extensions;
using CloudIn.Core.WebApi.Common.Settings;
using CloudIn.Core.WebApi.GraphQl.Schema;
using CloudIn.Core.WebApi.Providers;
using CloudIn.Core.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);
var settingsSection = builder.Configuration.GetRequiredSection("Settings");
var settingsValues = settingsSection.Get<AppSettings>();

builder.Services
    .AddCors()
    .AddHttpContextAccessor()
    .Configure<AppSettings>(settingsSection)
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

builder.Services.AddPlugins(path: settingsValues.PluginsDir);

var app = builder.Build();
await app.ApplyMigrationsAsync<DataContext>();

app.UseRouting();

app.UseCors(builder => builder
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin()
    .WithExposedHeaders(CorsHelper.GetExposedHeaders())
);

app.MapGraphQL(path: "/api/graphql").WithName("graphql");
app.MapUpload(path: "/api/upload", storePath: settingsValues.UploadTempDataDir ?? @"./tmp/uploads/");
app.MapDownload(path: "/api/media/{fileId}").WithName("download");

app.UsePlugins();

app.Run();

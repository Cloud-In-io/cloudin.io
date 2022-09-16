using CloudIn.Core.Data;
using Microsoft.EntityFrameworkCore;
using CloudIn.Core.Data;
using CloudIn.Core.WebApi.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(
    opt =>
        opt.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly("WebApi")
        )
);


var app = builder.Build();

await app.ApplyMigrationsAsync<DataContext>();


app.Run();

using CloudIn.Core.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services.AddDbContext<DataContext>(
        opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("WebApi")
    ));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

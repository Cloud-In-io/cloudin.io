using Microsoft.AspNetCore.Mvc;
using CloudIn.Core.ApplicationDomain.Contracts.Providers.FileSystemProvider;
using CloudIn.Core.ApplicationDomain.Contracts.Repositories;

namespace CloudIn.Core.WebApi.Middleware;

public static class DownloadMiddleware
{
    public static IEndpointConventionBuilder MapDownloadEndpoint(this WebApplication app, string path = "/api/media/{fileId}") => 
        app.MapGet(path, async (
            [FromRoute] Guid fileId, 
            [FromServices] IFileRepository fileRepository, 
            [FromServices] IFileSystemProvider fileSystemProvider
        ) => 
        {
            var file = await fileRepository.GetByIdAsync(fileId);

            if(file == null)
                return Results.NotFound();

            return Results.File(
                fileStream: await fileSystemProvider.OpenReadAsync(file.PhysicalPath),
                contentType: file.MimeType,
                fileDownloadName: file.Name
            );
        });
}
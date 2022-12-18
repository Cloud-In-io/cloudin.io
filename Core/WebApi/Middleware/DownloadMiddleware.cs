using MimeTypes;
using Microsoft.AspNetCore.Mvc;
using CloudIn.Core.Domain.Contracts.Providers.FileSystemProvider;
using CloudIn.Core.Domain.Contracts.Repositories;

namespace CloudIn.Core.WebApi.Middleware;

public static class DownloadMiddleware
{
    public static IEndpointConventionBuilder MapDownload(
        this IEndpointRouteBuilder endpoints,
        string path
    ) =>
        endpoints.MapGet(
            path,
            async (
                [FromRoute] Guid fileId,
                [FromServices] IFileRepository fileRepository,
                [FromServices] IFileSystemProvider fileSystemProvider
            ) =>
            {
                var file = await fileRepository.GetByIdAsync(fileId);

                if (file == null)
                    return Results.NotFound();

                var fileExtension = MimeTypeMap.GetExtension(file.MimeType);
                var downloadName = System.IO.Path.ChangeExtension(file.Name, fileExtension);

                return Results.File(
                    fileStream: await fileSystemProvider.OpenReadAsync(file.FilePath),
                    contentType: file.MimeType,
                    fileDownloadName: downloadName
                );
            }
        );
}

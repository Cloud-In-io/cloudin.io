using System.Net;
using System.Text;
using tusdotnet;
using tusdotnet.Models;
using tusdotnet.Stores;
using tusdotnet.Interfaces;
using tusdotnet.Models.Configuration;
using Microsoft.Extensions.Options;
using CloudIn.Core.WebApi.Common.Helpers;
using CloudIn.Core.WebApi.Common.Settings;
using CloudIn.Core.WebApi.Common.Contracts;
using CloudIn.Core.ApplicationDomain.Services.FileService.Interfaces;

namespace CloudIn.Core.WebApi.Middleware;

public static class UploadMiddleware
{
    public static IApplicationBuilder MapUploadEndpoint(this IApplicationBuilder app, string path = "/upload", string storePath = @"./tmp/uploads/") =>
        app.UseTus(httpContext => new DefaultTusConfiguration
        {
            UrlPath = path,
            Store = new TusDiskStore(storePath),
            Events = new Events
            {
                OnBeforeCreateAsync = (ctx) => 
                {
                    if (!ctx.HttpContext.Request.Headers.ContainsKey("Upload-Token"))
                    {
                        ctx.FailRequest("Upload-Token header was not provide or is invalid");
                    }

                    if (!ctx.Metadata.ContainsKey("name"))
                    {
                        ctx.FailRequest("name metadata must be specified.");
                    }

                    if (!ctx.Metadata.ContainsKey("contentType"))
                    {
                        ctx.FailRequest("contentType metadata must be specified.");
                    }
                    
                    return Task.CompletedTask;
                },
                OnFileCompleteAsync = async (ctx) =>
                {
                    var settings = httpContext.RequestServices.GetRequiredService<IOptions<AppSettings>>().Value;
                    var fileService = httpContext.RequestServices.GetRequiredService<IFileService>();

                    if(!ctx.HttpContext.Request.Headers.TryGetValue("Upload-Token", out var token))
                    {
                        throw new HttpRequestException(
                            message: "Upload-Token header was not provide or is invalid", 
                            statusCode: HttpStatusCode.Unauthorized, 
                            inner: null
                        );
                    }                  

                    var file = await ctx.GetFileAsync();
                    var metadata = await file.GetMetadataAsync(ctx.CancellationToken);
                    var payload = TokenHelper.ValidateToken<IUploadPayload>(token.ToString(), secret: settings.UploadJWTSecret);

                    if(payload == null)
                    {
                        throw new HttpRequestException(
                            message: "Upload data payload is invalid", 
                            statusCode: HttpStatusCode.BadRequest, 
                            inner: null
                        );
                    }

                    using(var content = await file.GetContentAsync(ctx.CancellationToken))
                    {
                        await fileService.WriteFileAsync(new IWriteFilePayload
                        {
                            OwnerUserId = payload.UserId,
                            ParentFolderId = payload.FolderId,
                            Name = payload.FileName,
                            Content = content,
                            MimeType = metadata["contentType"].GetString(Encoding.UTF8),
                        });
                    }

                    var terminationStore = (ITusTerminationStore)ctx.Store;
                    await terminationStore.DeleteFileAsync(ctx.FileId, ctx.CancellationToken);
                }
            }
        });
}
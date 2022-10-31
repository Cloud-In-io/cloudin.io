using System.Net;
using System.Text;
using tusdotnet;
using tusdotnet.Models;
using tusdotnet.Stores;
using tusdotnet.Interfaces;
using tusdotnet.Models.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using CloudIn.Core.WebApi.Common.Helpers;
using CloudIn.Core.WebApi.Common.Settings;
using CloudIn.Core.WebApi.Common.Contracts;
using CloudIn.Core.ApplicationDomain.Services.FileService.Interfaces;

namespace CloudIn.Core.WebApi.Middleware;

public static class UploadMiddleware
{
    public static IApplicationBuilder MapUploadEndpoint(this IApplicationBuilder app, string path = "/api/upload", string storePath = @"./tmp/uploads/") =>
        app.UseTus(httpContext => new DefaultTusConfiguration
        {
            UrlPath = path,
            Store = new TusDiskStore(storePath),
            Events = new Events
            {
                OnBeforeCreateAsync = (ctx) => 
                {
                    try
                    {
                        var settings = httpContext.RequestServices.GetRequiredService<IOptions<AppSettings>>().Value;

                        if(!ctx.HttpContext.Request.Query.TryGetValue("token", out var token)) 
                            throw new ArgumentException("the token parameter was not provide or is invalid.");
                        
                        TokenHelper.ValidateToken<IUploadPayload>(token.ToString(), secret: settings.UploadJWTSecret);

                        if (!ctx.Metadata.ContainsKey("name")) 
                            throw new ArgumentException("name metadata must be specified.");

                        if (!ctx.Metadata.ContainsKey("contentType")) 
                            throw new ArgumentException("contentType metadata must be specified.");
                    }
                    catch (System.ArgumentException e)
                    {
                        ctx.FailRequest(HttpStatusCode.BadRequest, e.Message);
                        //throw;
                    }
                    catch(System.Exception)
                    { 
                        ctx.FailRequest(HttpStatusCode.BadRequest, "invalid operation.");
                        //throw;
                    }

                    return Task.CompletedTask;
                },
                OnCreateCompleteAsync = (ctx) =>
                {
                    var queryParams = ctx.HttpContext.Request.QueryString.ToString();
                    var locationUri = new StringValues($"{path}/{ctx.FileId}{queryParams}");

                    ctx.HttpContext.Response.OnStarting(() => 
                    {
                        ctx.HttpContext.Response.Headers.Location = locationUri;
                        return Task.CompletedTask;
                    });

                    return Task.CompletedTask;
                },
                OnFileCompleteAsync = async (ctx) =>
                {
                    var settings = httpContext.RequestServices.GetRequiredService<IOptions<AppSettings>>().Value;
                    var fileService = httpContext.RequestServices.GetRequiredService<IFileService>();

                    var hasToken = ctx.HttpContext.Request.Query.TryGetValue("token", out var token);
                    var payload = TokenHelper.ValidateToken<IUploadPayload>(token.ToString(), secret: settings.UploadJWTSecret);

                    if(!hasToken || payload == null)
                    {
                        throw new HttpRequestException(
                            message: "token parameter was not provide or is invalid", 
                            statusCode: HttpStatusCode.Unauthorized, 
                            inner: null
                        );
                    }                  

                    var file = await ctx.GetFileAsync();
                    var metadata = await file.GetMetadataAsync(ctx.CancellationToken);

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
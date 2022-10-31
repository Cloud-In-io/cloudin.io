using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.WebUtilities;
using CloudIn.Core.WebApi.Common.Helpers;
using CloudIn.Core.WebApi.Common.Settings;
using CloudIn.Core.WebApi.GraphQl.Schema.InputTypes;

namespace CloudIn.Core.WebApi.GraphQl.Schema.Mutations;

[ExtendObjectType(typeof(BaseMutation))]
public class FileMutation
{
    public record PresignedUploadResult(string url);

    public PresignedUploadResult GetPresignedUpload(
        [Service] IHttpContextAccessor httpContextAccessor,
        [Service] IOptions<AppSettings> settingsProvider,
        PresignedUploadType presignedPayload
    )
    {
        var settings = settingsProvider.Value;
        var endpoint = "/api/upload";

        /* Do some validation */

        var token = TokenHelper.WriteToken(
            secret: settings.UploadJWTSecret,
            expires: settings.UploadExpirationSeconds,
            values: presignedPayload
        );

        var uploadUrl = QueryHelpers.AddQueryString(endpoint, "token", token);
        
        return new(uploadUrl);
    }
}

using Microsoft.Extensions.Options;
using CloudIn.Core.WebApi.Common.Helpers;
using CloudIn.Core.WebApi.Common.Settings;
using CloudIn.Core.WebApi.GraphQl.Schema.InputTypes;

namespace CloudIn.Core.WebApi.GraphQl.Schema.Mutations;

[ExtendObjectType(typeof(BaseMutation))]
public class FileMutation
{
    public record PresignedUploadResult(string Token);

    public PresignedUploadResult GetPresignedUpload(
        [Service] IHttpContextAccessor httpContextAccessor,
        [Service] IOptions<AppSettings> settingsProvider,
        PresignedUploadType presignedPayload
    )
    {
        var settings = settingsProvider.Value;

        /* Do some validation */

        var token = TokenHelper.WriteToken(
            secret: settings.UploadJWTSecret,
            expires: settings.UploadExpirationSeconds,
            values: presignedPayload
        );

        return new(token);
    }
}

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
        var baseUri = new Uri(WebHelper.GetBaseUri(httpContextAccessor) ?? string.Empty);

        /* Do some validation Here*/

        var token = TokenHelper.WriteToken(
            secret: settings.UploadJWTSecret,
            expires: settings.UploadExpirationSeconds,
            values: presignedPayload
        );

        var uploadUri = new Uri(baseUri, "/api/upload");

        var uploadUriWithQuery = QueryHelpers.AddQueryString(
            uri: uploadUri.ToString(),
            name: nameof(token),
            value: token
        );

        if (string.IsNullOrEmpty(uploadUriWithQuery))
        {
            throw new RouteCreationException("Could not create the upload route");
        }

        return new(uploadUriWithQuery);
    }
}

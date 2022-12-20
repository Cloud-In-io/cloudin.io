namespace CloudIn.Core.WebApi.Common.Settings;

public class AppSettings
{
    public string PluginsDir { get; set; } = default!;
    
    public string UploadDataDir { get; set; } = default!;

    public string? UploadTempDataDir { get; set; }

    public string AuthenticationJWTSecret { get; set; } = default!;

    public int AuthenticationExpirationSeconds { get; set; } = default!;

    public string UploadJWTSecret { get; set; } = default!;

    public int UploadExpirationSeconds { get; set; } = default!;
}
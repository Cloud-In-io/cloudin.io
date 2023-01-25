using System.ComponentModel.DataAnnotations;

namespace CloudIn.Core.WebApi.Common.Contracts;

public class IAuthenticatePayload
{
    [Required]
    [EmailAddress]
    public string Username { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;
}
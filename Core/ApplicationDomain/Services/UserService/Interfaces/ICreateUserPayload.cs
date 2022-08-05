using System.ComponentModel.DataAnnotations;

namespace ApplicationDomain.Services.UserService.Interfaces;

public class ICreateUserPayload
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = default!;

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = default!;

    [MaxLength(50)]
    public string? LastName { get; set; }

    [Required]
    [MaxLength(50)]
    public string Password { get; set; } = default!;
}

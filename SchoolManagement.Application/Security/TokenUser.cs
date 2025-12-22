namespace SchoolManagement.Application.Security;

public sealed class TokenUser
{
    public string UserId { get; init; } = default!;
    public string Email { get; init; } = default!;
}
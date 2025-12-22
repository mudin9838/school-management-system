namespace SchoolManagement.Api.Models.Auth;

public class TokenRevokeRequest
{
    public string RefreshToken { get; set; } = default!;
}
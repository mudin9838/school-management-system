namespace SchoolManagement.Api.Models.Auth;

public class TokenRefreshRequest
{
    public string RefreshToken { get; set; } = default!;
}

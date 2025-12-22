using SchoolManagement.Application.Security;

namespace SchoolManagement.Application.Interfaces.Security;

public interface IJwtTokenService
{
    string GenerateAccessToken(TokenUser user, IList<string> roles);
    string GenerateRefreshToken();
}
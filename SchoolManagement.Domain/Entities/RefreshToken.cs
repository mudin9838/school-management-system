namespace SchoolManagement.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; private set; }
    public string Token { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public bool IsRevoked { get; private set; }
    public string UserId { get; private set; }

    private RefreshToken() { }

    public RefreshToken(string token, DateTime expiresAt, string userId)
    {
        Id = Guid.NewGuid();
        Token = token;
        ExpiresAt = expiresAt;
        UserId = userId;
    }

    public void Revoke()
    {
        IsRevoked = true;
    }

    public bool IsActive()
        => !IsRevoked && DateTime.UtcNow < ExpiresAt;
}
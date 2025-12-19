namespace SchoolManagement.Api.Models.Auth;

public record RegisterRequest(
    string Email,
    string Password,
    string Role
);
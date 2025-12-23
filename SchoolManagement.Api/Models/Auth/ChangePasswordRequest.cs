namespace SchoolManagement.Api.Models.Auth;

public record ChangePasswordRequest(
    string CurrentPassword,
    string NewPassword
);

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Api.Models.Auth;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Application.Interfaces.Security;
using SchoolManagement.Application.Security;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.Identity;
namespace SchoolManagement.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        IJwtTokenService jwtTokenService,
        IRefreshTokenRepository refreshTokenRepository,
        ILogger<AuthController> logger)
    {
        _userManager = userManager;
        _jwtTokenService = jwtTokenService;
        _refreshTokenRepository = refreshTokenRepository;
        _logger = logger;
    }

    // ---------------- REGISTER ----------------
    //[HttpPost("register")]
    //public async Task<IActionResult> Register(RegisterRequest request)
    //{
    //    var user = new ApplicationUser
    //    {
    //        UserName = request.Email,
    //        Email = request.Email
    //    };

    //    var result = await _userManager.CreateAsync(user, request.Password);
    //    if (!result.Succeeded)
    //        return BadRequest(result.Errors);

    //    await _userManager.AddToRoleAsync(user, request.Role);

    //    return Ok("User registered successfully");
    //}

    // ---------------- LOGIN ----------------
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return Unauthorized("Invalid credentials");

            var validPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!validPassword)
                return Unauthorized("Invalid credentials");

            var roles = await _userManager.GetRolesAsync(user);

            var tokenUser = new TokenUser
            {
                UserId = user.Id,
                Email = user.Email!
            };

            // Debug logging
            _logger.LogInformation($"Generating token for user: {user.Email}");

            var accessToken = _jwtTokenService.GenerateAccessToken(tokenUser, roles);
            var refreshTokenValue = _jwtTokenService.GenerateRefreshToken();

            var refreshToken = new RefreshToken(
                refreshTokenValue,
                DateTime.UtcNow.AddDays(7),
                user.Id);

            await _refreshTokenRepository.AddAsync(refreshToken);
            await _refreshTokenRepository.SaveChangesAsync();

            return Ok(new
            {
                accessToken,
                refreshToken = refreshTokenValue
            });
        }
        catch (Exception ex)
        {
            // Log the full exception
            _logger.LogError(ex, "Error during login for email: {Email}", request.Email);
            return StatusCode(500, "An error occurred during login");
        }
    }

    // ---------------- REFRESH TOKEN ----------------
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(TokenRefreshRequest request)
    {
        var existingToken =
            await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken);

        if (existingToken == null || !existingToken.IsActive())
            return Unauthorized("Invalid refresh token");

        // Rotate token
        existingToken.Revoke();

        var user = await _userManager.FindByIdAsync(existingToken.UserId);
        if (user == null)
            return Unauthorized();

        var roles = await _userManager.GetRolesAsync(user);

        var tokenUser = new TokenUser
        {
            UserId = user.Id,
            Email = user.Email!
        };

        var accessToken = _jwtTokenService.GenerateAccessToken(tokenUser, roles);
        var newRefreshTokenValue = _jwtTokenService.GenerateRefreshToken();

        await _refreshTokenRepository.AddAsync(
            new RefreshToken(
                newRefreshTokenValue,
                DateTime.UtcNow.AddDays(7),
                user.Id));

        await _refreshTokenRepository.SaveChangesAsync();

        return Ok(new
        {
            accessToken = accessToken,
            refreshToken = newRefreshTokenValue
        });
    }

    // ---------------- LOGOUT ----------------
    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout(TokenRevokeRequest request)
    {
        var token =
            await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken);

        if (token != null)
        {
            token.Revoke();
            await _refreshTokenRepository.SaveChangesAsync();
        }

        return NoContent();
    }


}

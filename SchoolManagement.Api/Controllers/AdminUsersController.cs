using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Api.Models.Auth;
using SchoolManagement.Infrastructure.Identity;
using RegisterRequest = SchoolManagement.Api.Models.Auth.RegisterRequest;
namespace SchoolManagement.Api.Controllers;

[ApiController]
[Route("api/admin/users")]
[Authorize(Policy = "AdminOnly")]
public class AdminUsersController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminUsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = _userManager.Users.Select(u => new { u.Id, u.UserName, u.Email }).ToList();
        return Ok(users);

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        var roles = await _userManager.GetRolesAsync(user);
        return Ok(new { user.Id, user.UserName, user.Email, Roles = roles });
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] RegisterRequest request)
    {
        if (request.Roles is null || request.Roles.Count == 0)
            return BadRequest("At least one role must be selected.");

        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
            return BadRequest("User already exists.");

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email
        };

        var createResult = await _userManager.CreateAsync(user, request.Password);
        if (!createResult.Succeeded)
            return BadRequest(createResult.Errors);


        foreach (var role in request.Roles) { if (await _roleManager.RoleExistsAsync(role)) { await _userManager.AddToRoleAsync(user, role); } }

        return Ok("User created successfully.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, UpdateUserRequest request)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return NotFound();

        user.Email = request.Email;
        user.UserName = request.Email;

        await _userManager.UpdateAsync(user);

        return NoContent();
    }
    [HttpPost("{id}/roles/{role}")]
    public async Task<IActionResult> AssignRole(string id, string role)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return NotFound();

        if (!await _roleManager.RoleExistsAsync(role))
            return BadRequest("Role does not exist");

        await _userManager.AddToRoleAsync(user, role);
        return NoContent();
    }
    [HttpDelete("{id}/roles/{role}")]
    public async Task<IActionResult> RemoveRole(string id, string role)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return NotFound();

        await _userManager.RemoveFromRoleAsync(user, role);
        return NoContent();
    }
    [HttpGet("/api/admin/roles")]
    public IActionResult GetRoles()
    {
        var roles = _roleManager.Roles
            .Select(r => r.Name)
            .ToList();

        return Ok(roles);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return NotFound();

        user.LockoutEnabled = true;
        user.LockoutEnd = DateTimeOffset.MaxValue;

        await _userManager.UpdateAsync(user);

        return NoContent();
    }
    [HttpPost("{id}/lock")]
    public async Task<IActionResult> LockUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return NotFound();

        user.LockoutEnabled = true;
        user.LockoutEnd = DateTimeOffset.UtcNow.AddDays(30);

        await _userManager.UpdateAsync(user);

        return NoContent();
    }
    [HttpPost("{id}/unlock")]
    public async Task<IActionResult> UnlockUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return NotFound();

        user.LockoutEnd = null;

        await _userManager.UpdateAsync(user);

        return NoContent();
    }
    [HttpPost("{id}/reset-password")]
    public async Task<IActionResult> ResetPassword(
    string id,
    ResetPasswordRequest request)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return NotFound();

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        var result = await _userManager.ResetPasswordAsync(
            user,
            token,
            request.NewPassword);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return NoContent();
    }

}

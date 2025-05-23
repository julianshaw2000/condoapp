using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using CondoApp.Core.DTOs;
using CondoApp.Core.Entities;
using CondoApp.Core.Interfaces;
using CondoApp.Core.DTOs.Auth;
using AutoMapper;

namespace CondoApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public AuthController(ILogger<AuthController> logger,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IJwtService jwtService,
            IMapper mapper)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        // GET: api/auth
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("AuthController is working.");
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null)
                return Unauthorized("Invalid username or password");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
                return Unauthorized("Invalid username or password");

            var roles = await _userManager.GetRolesAsync(user);

            var payload = new UserTokenPayload
            {
                UserId = user.Id,
                UserName = user.UserName!,
                Roles = roles,
                // CondoId = user.CondoId
            };

            var token = _jwtService.GenerateToken(payload);

            return Ok(new { token });
        }

        // POST: api/auth/logout
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Logout successful.");
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _mapper.Map<ApplicationUser>(registerDto);

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // 🔐 Assign default role (Owner)
            await _userManager.AddToRoleAsync(user, "Owner");

            // 🔑 Generate token with role
            var payload = new UserTokenPayload
            {
                UserId = user.Id,
                UserName = user.UserName!,
                Roles = new List<string> { "Owner" },
                // CondoId = user.CondoId
            };

            var token = _jwtService.GenerateToken(payload);

            return Ok(new { token });
        }

        // POST: api/auth/reset-password
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
                return BadRequest("Invalid email address.");

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword);
            if (result.Succeeded)
                return Ok("Password has been reset successfully.");
            else
                return BadRequest(result.Errors);
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole(string userEmail, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null) return NotFound("User not found");

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok($"Role {roleName} assigned to {userEmail}");
        }

        [HttpPost("remove-role")]
        public async Task<IActionResult> RemoveRole(string userEmail, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null) return NotFound("User not found");

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok($"Role {roleName} removed from {userEmail}");
        }
    }
}
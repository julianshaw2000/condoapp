using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CondoApp.Core.Entities;
using CondoApp.Core.Interfaces;
using CondoApp.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CondoApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepo;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepo = _unitOfWork.Users;
        }

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _userRepo.GetAllAsync();
            // If you have a UserDTO, map here
            return Ok(new ApiResponse<List<ApplicationUser>>
            {
                Success = true,
                Message = "Fetched successfully",
                Data = users.ToList()
            });
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(string id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null)
            {
                return Ok(new ApiResponse<ApplicationUser>
                {
                    Success = false,
                    Message = "User not found"
                });
            }
            return Ok(new ApiResponse<ApplicationUser>
            {
                Success = true,
                Message = "Fetched successfully",
                Data = user
            });
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] ApplicationUser user)
        {
            await _userRepo.AddAsync(user);
            await _unitOfWork.CompleteAsync();
            // If ApplicationUser has Id, use it, else just return user
            return CreatedAtAction(nameof(GetUserByIdAsync), new { id = user.Id }, new ApiResponse<ApplicationUser>
            {
                Success = true,
                Message = "User created successfully",
                Data = user
            });
        }

        // PUT: api/User/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(string id, [FromBody] ApplicationUser user)
        {
            var existingUser = await _userRepo.GetByIdAsync(id);
            if (existingUser == null)
            {
                return Ok(new ApiResponse<object>
                {
                    Success = false,
                    Message = "User not found"
                });
            }
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            // Update other fields as needed
            _userRepo.Update(existingUser);
            await _unitOfWork.CompleteAsync();
            return Ok(new ApiResponse<ApplicationUser>
            {
                Success = true,
                Message = "User updated successfully",
                Data = existingUser
            });
        }

        // DELETE: api/User/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null)
            {
                return Ok(new ApiResponse<object>
                {
                    Success = false,
                    Message = "User not found"
                });
            }
            _userRepo.Delete(user);
            await _unitOfWork.CompleteAsync();
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "User deleted successfully"
            });
        }
    }
}
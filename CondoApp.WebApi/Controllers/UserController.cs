using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CondoApp.Core.Entities;
using CondoApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CondoApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            // Use reflection to call GenericRepository methods if needed
            var repo = _unitOfWork.Users as dynamic;
            var users = await repo.GetAllAsync();
            return Ok(users);
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var repo = _unitOfWork.Users as dynamic;
            var user = await repo.GetByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] ApplicationUser user)
        {
            var repo = _unitOfWork.Users as dynamic;
            await repo.AddAsync(user);
            await _unitOfWork.CompleteAsync();
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        // PUT: api/User/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] ApplicationUser user)
        {
            var repo = _unitOfWork.Users as dynamic;
            var existingUser = await repo.GetByIdAsync(id);
            if (existingUser == null)
                return NotFound();
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            // Update other fields as needed
            repo.Update(existingUser);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

        // DELETE: api/User/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var repo = _unitOfWork.Users as dynamic;
            var user = await repo.GetByIdAsync(id);
            if (user == null)
                return NotFound();
            repo.Delete(user);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
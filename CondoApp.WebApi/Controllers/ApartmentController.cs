using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CondoApp.Core.Entities;
using CondoApp.Core.Interfaces;
using CondoApp.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CondoApp.Core.DTOs.Entities;
using Microsoft.AspNetCore.Authorization;
using CondoApp.Data;

namespace CondoApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ApartmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApartmentRepository _apartmentRepo;
        private readonly IMapper _mapper;

        public ApartmentController(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            AppDbContext dbContext,
            ITenantProvider tenantProvider)
        {
            _unitOfWork = unitOfWork;
            _apartmentRepo = _unitOfWork.Apartments;
            _mapper = mapper;

            // 🟢 Assign tenant info for this request
            dbContext.CurrentTenantId = tenantProvider.GetTenantId();
            dbContext.IsSuperAdmin = tenantProvider.IsSuperAdmin();
        }

        [AllowAnonymous]
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Test endpoint is working!");
        }

        // GET: api/Apartment
        [HttpGet]
        public async Task<IActionResult> GetAllApartmentsAsync()
        {
            // Debug logs
            var isAuthenticated = User.Identity?.IsAuthenticated ?? false;
            var username = User.Identity?.Name;
            var roles = User.Claims
                .Where(c => c.Type == System.Security.Claims.ClaimTypes.Role)
                .Select(c => c.Value).ToList();

            Console.WriteLine($"Is Authenticated: {isAuthenticated}");
            Console.WriteLine($"Username: {username}");
            Console.WriteLine($"Roles: {string.Join(",", roles)}");

            var apartments = await _apartmentRepo.GetAllAsync();
            var apartmentDtos = _mapper.Map<List<ApartmentDTO>>(apartments);

            return Ok(new ApiResponse<List<ApartmentDTO>>
            {
                Success = true,
                Message = "Fetched successfully",
                Data = apartmentDtos
            });
        }

        // GET: api/Apartment/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApartmentByIdAsync(int id)
        {
            var apartment = await _apartmentRepo.GetByIdAsync(id);
            if (apartment == null)
            {
                return NotFound(new ApiResponse<ApartmentDTO>
                {
                    Success = false,
                    Message = "Apartment not found"
                });
            }

            var apartmentDto = _mapper.Map<ApartmentDTO>(apartment);
            return Ok(new ApiResponse<ApartmentDTO>
            {
                Success = true,
                Message = "Fetched successfully",
                Data = apartmentDto
            });
        }

        // POST: api/Apartment
        [HttpPost]
        public async Task<IActionResult> CreateApartmentAsync([FromBody] ApartmentDTO apartmentDto)
        {
            var apartmentEntity = _mapper.Map<Apartment>(apartmentDto);
            await _apartmentRepo.AddAsync(apartmentEntity);
            await _unitOfWork.CompleteAsync();

            var createdDto = _mapper.Map<ApartmentDTO>(apartmentEntity);
            return Ok(new ApiResponse<ApartmentDTO>
            {
                Success = true,
                Message = "Apartment created successfully",
                Data = createdDto
            });
        }

        // PUT: api/Apartment/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApartmentAsync(int id, [FromBody] ApartmentDTO apartmentDto)
        {
            var existingApartment = await _apartmentRepo.GetByIdAsync(id);
            if (existingApartment == null)
            {
                return Ok(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Apartment not found"
                });
            }

            _mapper.Map(apartmentDto, existingApartment);
            _apartmentRepo.Update(existingApartment);
            await _unitOfWork.CompleteAsync();

            var updatedDto = _mapper.Map<ApartmentDTO>(existingApartment);
            return Ok(new ApiResponse<ApartmentDTO>
            {
                Success = true,
                Message = "Apartment updated successfully",
                Data = updatedDto
            });
        }

        // DELETE: api/Apartment/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApartmentAsync(int id)
        {
            var apartment = await _apartmentRepo.GetByIdAsync(id);
            if (apartment == null)
            {
                return Ok(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Apartment not found"
                });
            }

            _apartmentRepo.Delete(apartment);
            await _unitOfWork.CompleteAsync();
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Apartment deleted successfully"
            });
        }
    }
}

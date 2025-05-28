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

namespace CondoApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TenantController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantRepository _tenantRepo;
        private readonly IMapper _mapper;

        public TenantController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _tenantRepo = _unitOfWork.Tenants;
            _mapper = mapper;
        }

        // GET: api/Tenant
        [HttpGet]
        public async Task<IActionResult> GetAllTenantsAsync()
        {
            var tenants = await _tenantRepo.GetAllAsync();
            var tenantDtos = _mapper.Map<List<TenantDTO>>(tenants);
            return Ok(new ApiResponse<List<TenantDTO>>
            {
                Success = true,
                Message = "Fetched successfully",
                Data = tenantDtos
            });
        }

        // GET: api/Tenant/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTenantByIdAsync(int id)
        {
            var tenant = await _tenantRepo.GetByIdAsync(id);
            if (tenant == null)
            {
                return Ok(new ApiResponse<TenantDTO>
                {
                    Success = false,
                    Message = "Tenant not found"
                });
            }
            var tenantDto = _mapper.Map<TenantDTO>(tenant);
            return Ok(new ApiResponse<TenantDTO>
            {
                Success = true,
                Message = "Fetched successfully",
                Data = tenantDto
            });
        }

        // POST: api/Tenant
        [HttpPost]
        public async Task<IActionResult> CreateTenantAsync([FromBody] TenantDTO tenantDto)
        {
            var tenantEntity = _mapper.Map<Tenant>(tenantDto);
            await _tenantRepo.AddAsync(tenantEntity);
            await _unitOfWork.CompleteAsync();
            var createdDto = _mapper.Map<TenantDTO>(tenantEntity);
            // If TenantDTO has Id, use it, else just return createdDto
            return CreatedAtAction(nameof(GetTenantByIdAsync), new { id = (object)createdDto.GetType().GetProperty("Id")?.GetValue(createdDto) }, new ApiResponse<TenantDTO>
            {
                Success = true,
                Message = "Tenant created successfully",
                Data = createdDto
            });
        }

        // PUT: api/Tenant/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTenantAsync(int id, [FromBody] TenantDTO tenantDto)
        {
            var existingTenant = await _tenantRepo.GetByIdAsync(id);
            if (existingTenant == null)
            {
                return Ok(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Tenant not found"
                });
            }
            _mapper.Map(tenantDto, existingTenant);
            _tenantRepo.Update(existingTenant);
            await _unitOfWork.CompleteAsync();
            var updatedDto = _mapper.Map<TenantDTO>(existingTenant);
            return Ok(new ApiResponse<TenantDTO>
            {
                Success = true,
                Message = "Tenant updated successfully",
                Data = updatedDto
            });
        }

        // DELETE: api/Tenant/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTenantAsync(int id)
        {
            var tenant = await _tenantRepo.GetByIdAsync(id);
            if (tenant == null)
            {
                return Ok(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Tenant not found"
                });
            }
            _tenantRepo.Delete(tenant);
            await _unitOfWork.CompleteAsync();
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Tenant deleted successfully"
            });
        }
    }
}

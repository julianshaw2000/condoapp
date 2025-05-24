use the following code as a  contoller template, make dtos and update the Automapper profile, 
 The DTO should contain fields only and no key id, use the Tenant model,  controller should be called TenantController:



using System
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CondoApp.Core.Entities;
using CondoApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CondoApp.Core.DTOs.Entities;
using CondoApp.WebApi.Models;

namespace CondoApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApartmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ApartmentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/Apartment
        [HttpGet]
        public async Task<IActionResult> GetAllApartments()
        {
            var repo = _unitOfWork.Apartments as dynamic;
            var apartments = await repo.GetAllAsync();
            return Ok(new ApiResponse<List<Apartment>>
            {
                Success = true,
                Message = "Fetched successfully",
                Data = apartments
            });
        }

        // GET: api/Apartment/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApartmentById(int id)
        {
            var repo = _unitOfWork.Apartments as dynamic;
            var apartment = await repo.GetByIdAsync(id);
            if (apartment == null)
                return NotFound(new ApiResponse<Apartment>
                {
                    Success = false,
                    Message = "Apartment not found",
                    Data = null
                });
            return Ok(new ApiResponse<Apartment>
            {
                Success = true,
                Message = "Fetched successfully",
                Data = apartment
            });

        }

        // POST: api/Apartment
        [HttpPost]
        public async Task<IActionResult> CreateApartment([FromBody] ApartmentDTO apartment)
        {
            var repo = _unitOfWork.Apartments;
            var apartmentEntity = _mapper.Map<Apartment>(apartment);
            await repo.AddAsync(apartmentEntity);
            await _unitOfWork.CompleteAsync();
            return Ok(new ApiResponse<Apartment>
            {
                Success = true,
                Message = "Apartment created successfully",
                Data = apartmentEntity
            });
        }

        // PUT: api/Apartment/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApartment(int id, [FromBody] ApartmentDTO apartment)
        {
            var repo = _unitOfWork.Apartments as dynamic;
            var existingApartment = await repo.GetByIdAsync(id);
            if (existingApartment == null)
                return NotFound(new ApiResponse<Apartment>
                {
                    Success = false,
                    Message = "Apartment not found",
                    Data = null
                });
            _mapper.Map(apartment, existingApartment);
            repo.Update(existingApartment);
            await _unitOfWork.CompleteAsync();
            return Ok(new ApiResponse<Apartment>
            {
                Success = true,
                Message = "Apartment updated successfully",
                Data = existingApartment
            });
        }

        // DELETE: api/Apartment/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApartment(int id)
        {
            var repo = _unitOfWork.Apartments as dynamic;
            var apartment = await repo.GetByIdAsync(id);
            if (apartment == null)
                return NotFound(new ApiResponse<Apartment>
                {
                    Success = false,
                    Message = "Apartment not found",
                    Data = null
                });
            repo.Delete(apartment);
            await _unitOfWork.CompleteAsync();
            return Ok(new ApiResponse<Apartment>
            {
                Success = true,
                Message = "Apartment deleted successfully",
                Data = apartment
            });
        }
    }
}


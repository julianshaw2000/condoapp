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
    public class PersonController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPersonRepository _personRepo;
        private readonly IMapper _mapper;

        public PersonController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _personRepo = _unitOfWork.Persons;
            _mapper = mapper;
        }

        // GET: api/Person
        [HttpGet]
        public async Task<IActionResult> GetAllPersonsAsync()
        {
            var persons = await _personRepo.GetAllAsync();
            var personDtos = _mapper.Map<List<PersonDTO>>(persons);
            return Ok(new ApiResponse<List<PersonDTO>>
            {
                Success = true,
                Message = "Fetched successfully",
                Data = personDtos
            });
        }

        // GET: api/Person/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonByIdAsync(int id)
        {
            var person = await _personRepo.GetByIdAsync(id);
            if (person == null)
            {
                return Ok(new ApiResponse<PersonDTO>
                {
                    Success = false,
                    Message = "Person not found"
                });
            }
            var personDto = _mapper.Map<PersonDTO>(person);
            return Ok(new ApiResponse<PersonDTO>
            {
                Success = true,
                Message = "Fetched successfully",
                Data = personDto
            });
        }

        // POST: api/Person
        [HttpPost]
        public async Task<IActionResult> CreatePersonAsync([FromBody] PersonDTO personDto)
        {
            var personEntity = _mapper.Map<Person>(personDto);
            await _personRepo.AddAsync(personEntity);
            await _unitOfWork.CompleteAsync();
            var createdDto = _mapper.Map<PersonDTO>(personEntity);
            // If PersonDTO has Id, use it, else just return createdDto
            return CreatedAtAction(nameof(GetPersonByIdAsync), new { id = (object)createdDto.GetType().GetProperty("Id")?.GetValue(createdDto) }, new ApiResponse<PersonDTO>
            {
                Success = true,
                Message = "Person created successfully",
                Data = createdDto
            });
        }

        // PUT: api/Person/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersonAsync(int id, [FromBody] PersonDTO personDto)
        {
            var existingPerson = await _personRepo.GetByIdAsync(id);
            if (existingPerson == null)
            {
                return Ok(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Person not found"
                });
            }
            _mapper.Map(personDto, existingPerson);
            _personRepo.Update(existingPerson);
            await _unitOfWork.CompleteAsync();
            var updatedDto = _mapper.Map<PersonDTO>(existingPerson);
            return Ok(new ApiResponse<PersonDTO>
            {
                Success = true,
                Message = "Person updated successfully",
                Data = updatedDto
            });
        }

        // DELETE: api/Person/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonAsync(int id)
        {
            var person = await _personRepo.GetByIdAsync(id);
            if (person == null)
            {
                return Ok(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Person not found"
                });
            }
            _personRepo.Delete(person);
            await _unitOfWork.CompleteAsync();
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Person deleted successfully"
            });
        }
    }
}

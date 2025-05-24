using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CondoApp.Core.DTOs.Auth;
using CondoApp.Core.DTOs.Entities;
using CondoApp.Core.Entities;

namespace CondoApp.Core.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDto, ApplicationUser>().ReverseMap();
            // .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            // .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            // .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName));
            CreateMap<LoginDto, ApplicationUser>().ReverseMap();
            CreateMap<ForgotPasswordDto, ApplicationUser>().ReverseMap();

            CreateMap<Apartment, ApartmentDTO>().ReverseMap();
            CreateMap<PersonDTO, Person>().ReverseMap();
            CreateMap<TenantDTO, Tenant>().ReverseMap();
        }
    }
}
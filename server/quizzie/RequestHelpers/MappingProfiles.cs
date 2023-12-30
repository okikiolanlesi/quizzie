using AutoMapper;
using Quizzie.DTOs;
using Quizzie.Models;

namespace Quizzie.RequestHelpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<RegisterDto, User>();
            CreateMap<User, UserDto>()
                 .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()))
                 .ReverseMap();
        }
    }
}

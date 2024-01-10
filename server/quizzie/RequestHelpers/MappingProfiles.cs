using System.Collections.Generic;
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
                 .ReverseMap();
            CreateMap<CreateOrUpdateCategoryDto, Category>().ReverseMap();
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<QuizDto, Quiz>().ReverseMap();
            CreateMap<Quiz, UserQuizDetailDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<Quiz, AdminQuizDetailDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Quizzes, opt => opt.MapFrom(src => src.Quizzes));


            CreateMap<Category, QuizCategoryDto>();
            CreateMap<Quiz, CategoryQuiz>();
            CreateMap<Question, QuestionDto>()
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Options));

            CreateMap<Option, UserOptionDto>();
            CreateMap<CreateQuestionDto, Question>().ReverseMap();
            CreateMap<CreateOrUpdateOptionDto, Option>().ReverseMap();
            CreateMap<AdminOptionDto, Option>().ReverseMap();
            CreateMap<AdminQuestionDto, Question>().ReverseMap();


        }
    }
}

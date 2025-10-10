using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SkillBridges.Data;
using SkillBridges.Models;
using SkillBridges.ViewModels;

namespace SkillBridges.Mappings
{
    public class Helper:Profile
    {
        public Helper()
        {
            
            CreateMap< UserCreateViewModel,User>();
            CreateMap<User,UserViewModel>();
            CreateMap<User, UserEditViewModel>().ReverseMap();
           
            CreateMap<ProfessionalProfile, ProfessionalEditViewModel>();
            CreateMap<ProfessionalEditViewModel, ProfessionalProfile>().ForMember(dest => dest.UserId, opt => opt.Ignore()).ForMember(dest=>dest.ProfessionalProfileId,opt=>opt.Ignore());
            CreateMap<ClientProfile,ClientEditViewModel>().ReverseMap().ForMember(dest=>dest.UserId,opt=>opt.Ignore());
            CreateMap<ClientProfile, ClientDetailsViewModel>();
            CreateMap<ProfessionalProfile,ProfessionalDetailsViewModel>();

            CreateMap<Models.Task, TaskEditViewModel>().ReverseMap();
            CreateMap<Models.Task, TaskCreateViewModel>().ReverseMap();
            CreateMap<Models.Task, TaskViewModel>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.ClientProfile.OrganizationName))
                .ForMember(dest=>dest.Email,opt=>opt.MapFrom(src=>src.ClientProfile.User.Email))
                .ForMember(dest=>dest.ProfessionalName,opt=>opt.MapFrom(src=>src.ProfessionalProfile.User.Name))
                .ForMember(dest=>dest.CategoryName,opt=>opt.MapFrom(src=>src.Category.Name))
                .ReverseMap();

            CreateMap<TaskApplicationCreateViewModel, TaskApplication>();
            CreateMap<TaskApplication, TaskApplicationViewModel>().ForMember(dest=>dest.TaskTitle,opt=>opt.MapFrom(src=>src.Task.Title))
                .ForMember(dest=>dest.ClientName,opt=>opt.MapFrom(src=>src.ClientProfile.OrganizationName))
                .ForMember(dest=>dest.ProfessionalName,opt=>opt.MapFrom(src=>src.ProfessionalProfile!=null ? src.ProfessionalProfile.User.Name :"UnAssigned"))
                .ForMember(dest=>dest.Skills,opt=>opt.MapFrom(src=>src.ProfessionalProfile.Skills.Select(s=>s.Skill.Name).ToList()));

            CreateMap<SkillViewModel, Skill>().ReverseMap();
            CreateMap<SkillCreateViewModel, Skill>();

            CreateMap<CategoryViewModel,Category>().ReverseMap();
            CreateMap<CategoryCreateViewModel, Category>();

            CreateMap<RatingCreateViewModel, Rating>();
            CreateMap<Rating, RatingViewModel>()
                .ForMember(dest => dest.TaskName, opt => opt.MapFrom(src => src.Task.Title))
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.ClientProfile.OrganizationName));
                

            CreateMap< TaskMessageCreateViewModel,TaskMessage>();
            CreateMap<TaskMessage, TaskMessageViewModel>()
                .ForMember(dest => dest.TaskTitle, opt => opt.MapFrom(src =>
                    src.Task.Title
                ));

            CreateMap<Payment, PaymentViewModel>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.ClientProfile.OrganizationName))
                .ForMember(dest => dest.TaskName, opt => opt.MapFrom(src => src.Task.Title))
                .ForMember(dest => dest.ProfessionalName, opt => opt.MapFrom(src => src.ProfessionalProfile.User.Name));

        }  
    }
}

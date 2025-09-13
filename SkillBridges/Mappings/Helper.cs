using AutoMapper;
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
            CreateMap<User, UserEditViewModel>();
            CreateMap<UserEditViewModel,User>().ForMember(dest=>dest.Role,opt=>opt.Ignore());
            CreateMap<ProfessionalProfile, ProfessionalEditViewModel>();
            CreateMap<ProfessionalEditViewModel, ProfessionalProfile>().ForMember(dest => dest.UserId, opt => opt.Ignore()).ForMember(dest=>dest.ProfessionalProfileId,opt=>opt.Ignore());
            CreateMap<ClientProfile,ClientEditViewModel>().ReverseMap().ForMember(dest=>dest.UserId,opt=>opt.Ignore());
            CreateMap<ClientProfile, ClientDetailsViewModel>();
            CreateMap<ProfessionalProfile,ProfessionalDetailsViewModel>();
        }  
    }
}

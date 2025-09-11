using AutoMapper;
using SkillBridges.Models;
using SkillBridges.ViewModels;

namespace SkillBridges.Mappings
{
    public class Helper:Profile
    {
        public Helper()
        {
            CreateMap<User, ProfessionalUserCreateViewModel>().ReverseMap();
        }
        
    }
}

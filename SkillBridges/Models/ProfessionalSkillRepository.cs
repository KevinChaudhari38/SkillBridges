using Microsoft.Build.Construction;
using Microsoft.EntityFrameworkCore;
using SkillBridges.Data;

namespace SkillBridges.Models
{
    public class ProfessionalSkillRepository:IProfessionalSkillRepository
    {
        private readonly SkillBridgeContext _context;
        public ProfessionalSkillRepository(SkillBridgeContext context)
        {
            _context = context;
        }
        public void Insert(ProfessionalSkill skill) { 
            _context.ProfessionalSkills.Add(skill);
        }
        public void Delete(ProfessionalSkill professionalSkill)
        {
            _context.ProfessionalSkills.Remove(professionalSkill);
        }
        public List<ProfessionalSkill> GetByProfessionalId(string professionalId)
        {
            return _context.ProfessionalSkills.Include(ps => ps.Skill).Where(ps => ps.ProfessionalProfileId == professionalId).ToList(); 
        }
        public List<ProfessionalSkill> GetBySkillId(string SkillId)
        {
            return _context.ProfessionalSkills.Include(ps => ps.Skill).Where(ps => ps.SkillId == SkillId).ToList();
        }
    }
}

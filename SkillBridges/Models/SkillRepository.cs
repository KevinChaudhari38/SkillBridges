using Microsoft.EntityFrameworkCore;
using SkillBridges.Data;

namespace SkillBridges.Models
{
    public class SkillRepository:ISkillRepository
    {
        private readonly SkillBridgeContext _context;
        public SkillRepository(SkillBridgeContext context) { 
            _context = context;
        }
        public Skill GetById(string id)
        {
            return _context.Skills.FirstOrDefault(e => e.SkillId == id);
        }
        public List<Skill> GetByProfessionalId(string id)
        {
            return _context.Skills.Include(c => c.ProfessionalSkills).ThenInclude(c=>c.ProfessionalProfile).Where(s=>s.ProfessionalSkills.Any(ps=>ps.ProfessionalProfileId==id)).ToList();
        }
        public List<Skill> GetAll()
        {
            return _context.Skills.ToList();
        }
        public void Insert(Skill skill)
        {
            skill.SkillId = Guid.NewGuid().ToString();
            _context.Skills.Add(skill);
        }
        public void Update(Skill skill) { 
            _context.Skills.Update(skill);
        }
        public void Delete(Skill skill)
        {
            _context.Skills.Remove(skill);
        }
    }
}

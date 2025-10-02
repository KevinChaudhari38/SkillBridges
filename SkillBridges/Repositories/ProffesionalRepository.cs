using Microsoft.EntityFrameworkCore;
using SkillBridges.Data;
using SkillBridges.Models;

namespace SkillBridges.Repositories
{
    public class ProfessionalRepository : IProfessionalRepository
    {
        private readonly SkillBridgeContext _context;

        public ProfessionalRepository(SkillBridgeContext context)
        {
            _context = context;
        }

        public ProfessionalProfile GetById(string id)
        {
            return _context.ProfessionalProfiles
                .Include(p => p.User)
                
                .FirstOrDefault(p => p.ProfessionalProfileId == id);
        }
        
        public ProfessionalProfile GetByUserId(string id)
        {
            return _context.ProfessionalProfiles.Include(p => p.User)
                                                .FirstOrDefault(p => p.UserId == id);
        }
        

        public List<ProfessionalProfile> GetAll()
        {
            return _context.ProfessionalProfiles.Include(p => p.Skills).ToList();
        }

        public void insert(ProfessionalProfile profile)
        {
            profile.ProfessionalProfileId = Guid.NewGuid().ToString();
            _context.ProfessionalProfiles.Add(profile);
        }

        public void update(ProfessionalProfile profile)
        {
            _context.Update(profile);
        }

        public void delete(ProfessionalProfile profile)
        {
            _context.ProfessionalProfiles.Remove(profile);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using SkillBridges.Data;

namespace SkillBridges.Models
{
    public class ProfessionalRepository : IProfessionalRepository
    {
        private readonly SkillBridgeContext _context;

        public ProfessionalRepository(SkillBridgeContext context)
        {
            this._context = context;
        }

        public ProfessionalProfile GetById(int id)
        {
            return _context.ProfessionalProfiles
                .Include(p => p.User)
                .FirstOrDefault(p => p.ProfessionalProfileId == id);
        }

        public List<ProfessionalProfile> GetAll()
        {
            return _context.ProfessionalProfiles.ToList();
        }

        public void insert(ProfessionalProfile profile)
        {
            _context.ProfessionalProfiles.Add(profile);
            _context.SaveChanges();
        }

        public void update(ProfessionalProfile profile)
        {
            _context.ProfessionalProfiles.Update(profile);
            _context.SaveChanges();
        }

        public void delete(ProfessionalProfile profile)
        {
            _context.ProfessionalProfiles.Remove(profile);
            _context.SaveChanges();
        }
    }
}

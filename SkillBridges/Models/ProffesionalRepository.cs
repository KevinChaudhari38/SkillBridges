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

        public ProfessionalProfile GetById(String id)
        {
            return _context.ProfessionalProfiles
                .Include(p => p.User)
                .FirstOrDefault(p => p.ProfessionalProfileId == id);
        }
        public ProfessionalProfile GetByUserId(String id)
        {
            return _context.ProfessionalProfiles.Include(p=>p.User).FirstOrDefault(p=>p.UserId==id);
        }

        public List<ProfessionalProfile> GetAll()
        {
            return _context.ProfessionalProfiles.ToList();
        }

        public void insert(ProfessionalProfile profile)
        {
            profile.ProfessionalProfileId=Guid.NewGuid().ToString();
            _context.ProfessionalProfiles.Add(profile);
            _context.SaveChanges();
        }

        public void update(ProfessionalProfile profile)
        {
       
            var existing = _context.ProfessionalProfiles
                           .FirstOrDefault(p => p.UserId == profile.ProfessionalProfileId);

            if (existing == null)
            {
                throw new Exception("Profile not found with Id :- "+ profile.ProfessionalProfileId);
            }

            existing.Bio = profile.Bio;
            existing.Location = profile.Location;
            existing.Languages = profile.Languages;
            existing.IsAvailable = profile.IsAvailable;

            _context.SaveChanges();
        }

        public void delete(ProfessionalProfile profile)
        {
            _context.ProfessionalProfiles.Remove(profile);
            _context.SaveChanges();
        }
    }
}

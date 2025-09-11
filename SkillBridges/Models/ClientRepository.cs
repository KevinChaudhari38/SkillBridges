using Microsoft.EntityFrameworkCore;
using SkillBridges.Data;

namespace SkillBridges.Models
{
    public class ClientRepository:IClientRepository
    {
        private readonly SkillBridgeContext _context;
        public ClientRepository(SkillBridgeContext context) { 
               this._context = context;
        }
        public ClientProfile GetById(int id) {
            return _context.ClientProfiles.Include(c=>c.User).FirstOrDefault(p=>p.ClientProfileId==id);
        }
        public List<ClientProfile> GetAll()
        {
            return _context.ClientProfiles.ToList();
        }
        public void insert(ClientProfile profile) {
            _context.ClientProfiles.Add(profile);
            _context.SaveChanges();
        }
        public void update(ClientProfile profile) { 
            _context.ClientProfiles.Update(profile);
            _context.SaveChanges();
        }
        public void delete(ClientProfile profile)
        {
            _context.ClientProfiles.Remove(profile);
            _context.SaveChanges();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using SkillBridges.Data;

namespace SkillBridges.Models
{
    public class ClientRepository : IClientRepository
    {
        private readonly SkillBridgeContext _context;
        public ClientRepository(SkillBridgeContext context)
        {
            this._context = context;
        }
        public ClientProfile GetById(String userId)
        {
            return _context.ClientProfiles.Include(c => c.User).FirstOrDefault(p => p.ClientProfileId == userId);
        }
        public ClientProfile GetByUserId(String id)
        {
            return _context.ClientProfiles.Include(c => c.User).FirstOrDefault(p => p.UserId == id);
        }
        public List<ClientProfile> GetAll()
        {
            return _context.ClientProfiles.ToList();
        }
        public void insert(ClientProfile profile)
        {
            profile.ClientProfileId = Guid.NewGuid().ToString();
            _context.ClientProfiles.Add(profile);
        }
        public void update(ClientProfile profile)
        {
            _context.ClientProfiles.Update(profile);
        }
        public void delete(ClientProfile profile)
        {
            _context.ClientProfiles.Remove(profile);
        }
    }
}

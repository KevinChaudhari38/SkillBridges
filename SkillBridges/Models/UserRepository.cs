using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using SkillBridges.Data;

namespace SkillBridges.Models
{
    public class UserRepository:IUserRepository
    {
        private readonly SkillBridgeContext _context;
        
        public UserRepository(SkillBridgeContext context)
        {
            _context = context;
        }

        public User GetById(String id)
        {
            return _context.Users.Include(u => u.ClientProfile).Include(p => p.ProfessionalProfile).FirstOrDefault(u => u.Id == id);
        }

        public void insert(User user)
        {
            user.Id = Guid.NewGuid().ToString();
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public void update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
        public void delete(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }
        public IEnumerable<SelectListItem> GetRoles()
        {
            return Enum.GetValues(typeof(UserRole))
                .Cast<UserRole>()
                .Select(r => new SelectListItem
                {
                    Value = ((int)r).ToString(),
                    Text = r.ToString()
                });
        }
        public User GetByEmailAndPassword(String Email,string Password)
        {
            return _context.Users.FirstOrDefault(u => u.Email == Email && u.PasswordHash == Password);
        }
    }
}


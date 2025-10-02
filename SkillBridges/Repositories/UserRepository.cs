using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using SkillBridges.Data;
using SkillBridges.Models;

namespace SkillBridges.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SkillBridgeContext _context;

        public UserRepository(SkillBridgeContext context)
        {
            _context = context;
        }

        public User GetById(string id)
        {
            return _context.Users
                           .Include(u => u.ClientProfile)
                           .Include(p => p.ProfessionalProfile)
                           .FirstOrDefault(u => u.Id == id);
        }
        
        public void insert(User user)
        {
            user.Id = Guid.NewGuid().ToString();
            _context.Users.Add(user);
        }

        public void update(User user)
        {
            _context.Users.Update(user);
        }

        public void delete(User user)
        {
            _context.Users.Remove(user);
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public IEnumerable<SelectListItem> GetRoles()
        {
            return Enum.GetValues(typeof(ViewModels.UserRole))
                       .Cast<UserRole>()
                       .Select(r => new SelectListItem
                       {
                           Value = ((int)r).ToString(),
                           Text = r.ToString()
                       });
        }
        

        public User GetByEmailAndPassword(string Email, string Password)
        {
            return _context.Users.FirstOrDefault(u => u.Email == Email && u.Password == Password);
        }
        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public void SetPasswordResetToken(User user,string token,DateTime expiration)
        {
            user.ResetPasswordToken = token;
            user.ResetPasswordTokenExpiry = expiration;
            _context.Users.Update(user);
        }
      
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
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

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public void insert(User user)
        {
            _context.Users.Add(user);
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


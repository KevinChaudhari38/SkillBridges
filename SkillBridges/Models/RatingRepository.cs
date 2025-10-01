using Microsoft.EntityFrameworkCore;
using SkillBridges.Data;

namespace SkillBridges.Models
{
    public class RatingRepository:IRatingRepository
    {
        private readonly SkillBridgeContext _context;
        public RatingRepository(SkillBridgeContext context)
        {
            _context = context;
        }
        public void Insert(Rating rating)
        {
            rating.RatingId = Guid.NewGuid().ToString();
            _context.Ratings.Add(rating);
        }
        public List<Rating> GetByProfessionalId(string ProfessionalProfileId)
        {
            return _context.Ratings.Include(c=>c.Task).Include(c=>c.ClientProfile).Where(t=>t.ProfessionalProfileId==ProfessionalProfileId).ToList();  
        }
        public Rating GetByTaskId(string TaskId)
        {
            return _context.Ratings.FirstOrDefault(t => t.TaskId == TaskId);
        }
    }
}

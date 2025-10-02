using SkillBridges.Data;
using SkillBridges.Models;

namespace SkillBridges.Repositories
{
    public interface IRatingRepository
    {
        void Insert(Rating rating);
        List<Rating> GetByProfessionalId(string  ProfessionalProfileId);
        Rating GetByTaskId(string TaskId);
    }
}

using SkillBridges.Data;

namespace SkillBridges.Models
{
    public interface IRatingRepository
    {
        void Insert(Rating rating);
        List<Rating> GetByProfessionalId(string  ProfessionalProfileId);
        Rating GetByTaskId(string TaskId);
    }
}

namespace SkillBridges.Models
{
    public interface IProfessionalRepository
    {
        ProfessionalProfile GetById(string id);
        ProfessionalProfile GetByUserId(string userId);
        List<ProfessionalProfile> GetAll();
        void insert(ProfessionalProfile profile);
        void update(ProfessionalProfile profile);
        void delete(ProfessionalProfile profile);
    }
}

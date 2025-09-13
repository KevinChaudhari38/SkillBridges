namespace SkillBridges.Models
{
    public interface IProfessionalRepository
    {
        ProfessionalProfile GetById(String id);
        ProfessionalProfile GetByUserId(String userId);
        List<ProfessionalProfile> GetAll();
        void insert(ProfessionalProfile profile);
        void update(ProfessionalProfile profile);
        void delete(ProfessionalProfile profile);
    }
}

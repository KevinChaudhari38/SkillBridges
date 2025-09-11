namespace SkillBridges.Models
{
    public interface IProfessionalRepository
    {
        ProfessionalProfile GetById(int id);
        List<ProfessionalProfile> GetAll();
        void insert(ProfessionalProfile profile);
        void update(ProfessionalProfile profile);
        void delete(ProfessionalProfile profile);
    }
}

namespace SkillBridges.Models
{
    public interface IProfessionalSkillRepository
    {
        void Insert(ProfessionalSkill professionalSkill);
        void Delete(ProfessionalSkill professionalSkill);
        List<ProfessionalSkill> GetByProfessionalId(string professionalId);
    }
}

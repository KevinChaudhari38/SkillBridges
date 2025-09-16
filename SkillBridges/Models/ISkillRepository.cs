namespace SkillBridges.Models
{
    public interface ISkillRepository
    {
        Skill GetById(string id);
        List<Skill> GetByProfessionalId(string id);
        List<Skill> GetAll();
        void Insert(Skill skill);
        void Update(Skill skill);
        void Delete(Skill skill);
    }
}

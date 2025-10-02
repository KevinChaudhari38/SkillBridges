using SkillBridges.Models;

namespace SkillBridges.Repositories
{
    public interface IPaymentRepository
    {
        List<Payment> GetByClientId(string ClientProfileId);
        List<Payment> GetByProfProfileId(string ProfessionalProfileId);
         
        List<Payment> GetByTaskId(string TaskId);

        void insert(Payment payment);

       
 

    }
}

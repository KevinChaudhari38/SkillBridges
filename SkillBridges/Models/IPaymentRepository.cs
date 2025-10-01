namespace SkillBridges.Models
{
    public interface IPaymentRepository
    {
        List<Payment> GetByClientId(string ClientProfileId);
        List<Payment> GetByProfProfileId(string ProfessionalProfileId);
         
        List<Payment> GetByTaskId(string TaskId);

        void insert(Payment payment);

       
 

    }
}

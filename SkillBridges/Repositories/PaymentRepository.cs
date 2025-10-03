using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SkillBridges.Data;
using SkillBridges.Models;

namespace SkillBridges.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly SkillBridgeContext _context;
        public PaymentRepository(SkillBridgeContext context)
        {
            _context = context;
        }

        public void insert(Payment payment) {
            if (string.IsNullOrEmpty(payment.PaymentId))
            {
                payment.PaymentId = Guid.NewGuid().ToString();
            }

            payment.CreatedAt = DateTime.UtcNow;
            _context.Payments.Add(payment);
        }

        public List<Payment> GetByClientId(string ClientProfileId) { 
        return _context.Payments
                .Include(p => p.Task)
                .Include(p => p.ProfessionalProfile)
                .ThenInclude(u=>u.User)
                .Include(p => p.ClientProfile)
                .Where(p => p.ClientProfileId == ClientProfileId)
                .OrderByDescending(p => p.CreatedAt)
                .ToList();

        }

        public List<Payment> GetByProfProfileId(string ProfessionalProfileId)
        {
            return _context.Payments
                .Include(p => p.Task)
                .Include(p => p.ClientProfile)
                .Include(p=>p.ProfessionalProfile)
                .ThenInclude(u => u.User)
                .Where(p => p.ProfessionalProfileId == ProfessionalProfileId)
                .OrderByDescending(p => p.CreatedAt)
                .ToList();
        }


        public List<Payment> GetByTaskId(string TaskId)
        {
            return _context.Payments
                .Include(p => p.Task)
                .Include(p=>p.ClientProfile)
                .Include(p=>p.ProfessionalProfile)
                .ThenInclude(u => u.User)
                .Where(p => p.TaskId == TaskId)
                .OrderByDescending(p => p.CreatedAt)
                .ToList();
        }
    }

}
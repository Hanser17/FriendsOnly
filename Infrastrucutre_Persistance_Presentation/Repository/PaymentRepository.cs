
using Application.IRepository;
using Domain.Entities;
using Infrastrucutre_Persistance_Presentation.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastrucutre_Persistance_Presentation.Repository
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        private readonly NetBankingContext _NetBankingContext;
        public PaymentRepository(NetBankingContext NetBankingContext) : base(NetBankingContext)
        {
            _NetBankingContext = NetBankingContext;
        }

        public async Task<decimal> GetDailyPayments()
        {
            var today = DateTime.Today;
            return await _NetBankingContext.Payments.Where(p => p.Date.Date == today).CountAsync();
        }

        public async Task<decimal> GetTotalPayments()
        {
            return await _NetBankingContext.Payments.CountAsync();
        }
    }
}

using Application.IRepository;
using Domain.Entities;
using Infrastrucutre_Persistance_Presentation.DBContext;
using Microsoft.EntityFrameworkCore;


namespace Infrastrucutre_Persistance_Presentation.Repository
{
    public class TransferRepository : GenericRepository<Transfer>, ITransferRepository
    {
        private readonly NetBankingContext _context;
        public TransferRepository(NetBankingContext context) : base(context)
        {
            _context = context;
        }

        public async  Task<int> GetTotalTransactions()
        {
            return await _context.Transfers.CountAsync();
        }
        

        public async Task<int> GetDailyTransactions()
        {
            var Today = DateTime.Today;
            return await _context.Transfers.Where(t =>t.Date.Date == Today).CountAsync();

        }
    }
}

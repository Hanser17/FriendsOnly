using Application.IRepository;
using Domain.Entities;
using Infrastrucutre_Persistance_Presentation.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastrucutre_Persistance_Presentation.Repository
{
    public class SavingsAccountRepository : GenericRepository<SavingsAccount>, ISavingsAccountRepository
    {
        private readonly NetBankingContext _netBankingContext;
        public SavingsAccountRepository(NetBankingContext NetBankingContext) : base(NetBankingContext)
        {
            _netBankingContext = NetBankingContext;     
        
        }

        
        public async Task<SavingsAccount> GetAccountByUserId(string userId)
        {
            var account = await _netBankingContext.SavingsAccounts.AsNoTracking().FirstOrDefaultAsync(sa => sa.UserId == userId && sa.IsPrimary == true);

            return account;
        } 
    }
}

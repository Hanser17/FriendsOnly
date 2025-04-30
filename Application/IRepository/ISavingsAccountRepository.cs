using Domain.Entities;


namespace Application.IRepository
{
    public interface ISavingsAccountRepository : IGenericRepository<SavingsAccount>
    {
        Task<SavingsAccount> GetAccountByUserId(string userId);

    }
}



using Domain.Entities;

namespace Application.IRepository
{
    public interface ITransferRepository : IGenericRepository<Transfer>
    {
        Task<int> GetTotalTransactions();
        Task<int> GetDailyTransactions();
    }
}

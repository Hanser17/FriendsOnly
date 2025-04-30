

using Application.DTOs.Transfer;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITransferService : IGenericService<TransferSaveDTO, TransferDTO, Transfer>
    {
        Task<int> GetTotalTransactions();
        Task<int> GetDailyTransactions();
    }
}

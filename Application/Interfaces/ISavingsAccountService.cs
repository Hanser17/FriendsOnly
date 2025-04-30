using Application.DTOs.SavingsAccount;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ISavingsAccountService : IGenericService<SavingsAccountSaveDTO, SavingsAccountDTO, SavingsAccount>
    {
        Task<SavingsAccountSaveDTO> GetbyUserId(string userId);
        Task UpdateBalance(string? userId, decimal amount, int? account);
    }
}

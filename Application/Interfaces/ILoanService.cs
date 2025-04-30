using Application.DTOs.Loan;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ILoanService :IGenericService<LoanSaveDTO, LoanDTO, Loan>
    {
        Task UpdateDebt(string? userId, decimal amount, int? account);
    }
}

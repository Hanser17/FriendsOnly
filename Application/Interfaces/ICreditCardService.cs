using Application.DTOs.CreditCard;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICreditCardService :IGenericService<CreditCardSaveDTO, CreditCardDTO, CreditCard>
    {
        Task UpdateDebt(string? userId, decimal amount, int? account);
    }
}

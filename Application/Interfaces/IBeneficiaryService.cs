using Application.DTOs.Beneficiary;
using Application.ViewModels.Beneficiary;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IBeneficiaryService :IGenericService<BeneficiarySaveDTO, BeneficiaryDTO, Beneficiary>
    {
        Task<List<BeneficiaryVM>> GetAllDto(string userId);
    }
}

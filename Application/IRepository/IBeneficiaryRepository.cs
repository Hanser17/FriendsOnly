using Application.DTOs.Beneficiary;
using Application.ViewModels.Beneficiary;
using Domain.Entities;


namespace Application.IRepository
{
    public interface IBeneficiaryRepository :IGenericRepository<Beneficiary>
    {
        Task<List<BeneficiaryVM>> GetAllDto(string userId);
    }
}

using Application.DTOs.Beneficiary;
using Application.Interfaces;
using Application.IRepository;
using Application.ViewModels.Beneficiary;
using AutoMapper;
using Domain.Entities;

namespace Application.Service
{
    public class BeneficiaryService : GenericService<BeneficiarySaveDTO, BeneficiaryDTO, Beneficiary>, IBeneficiaryService
    {
        private readonly IBeneficiaryRepository _repository;
        private readonly IMapper _mapper;
        public BeneficiaryService(IBeneficiaryRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
         
        public async Task<BeneficiarySaveDTO> AddAService (BeneficiarySaveDTO dto)
        {
            var add = _mapper.Map<Beneficiary>(dto);
            await _repository.Add(add);
            return dto;
        }
        

        public async Task<List<BeneficiaryVM>> GetAllDto(string userId)
        {
            var dataList = await _repository.GetAllDto(userId);
            return dataList;
        }
    }
}

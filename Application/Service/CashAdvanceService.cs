

using Application.DTOs.CashAdvance;
using Application.Interfaces;
using Application.IRepository;
using AutoMapper;
using Domain.Entities;

namespace Application.Service
{
    public class CashAdvanceService : GenericService<CashAdvanceSaveDTO, CashAdvanceDTO, CashAdvance>, ICashAdvanceService
    {
       private readonly ICashAdvanceRepository _cashAdvanceRepository;
        private readonly IMapper _mapper;

        public CashAdvanceService(ICashAdvanceRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _cashAdvanceRepository = repository;
            _mapper = mapper;

        }

       
    }
}

using Application.DTOs.Loan;
using Application.DTOs.SavingsAccount;
using Application.Interfaces;
using Application.IRepository;
using Application.ViewModels.Loan;
using AutoMapper;
using Domain.Entities;

namespace Application.Service
{
    public class LoanService : GenericService<LoanSaveDTO, LoanDTO, Loan>, ILoanService
    {
        private readonly ILoanRepository _Repository;
        private readonly IMapper _mapper;
        public LoanService(ILoanRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _mapper = mapper;
            _Repository = repository;
        }
        public async Task<LoanSaveDTO> AddService(LoanSaveDTO dto)
        {
            var Loan = _mapper.Map<Loan>(dto);
            await _Repository.Add(Loan);
            return dto;
        }
        public async Task<List<LoanDTO>> GetAllService(string userId)
        {
            var loansList = await _Repository.GetAll(userId);
            var loansListDTO = _mapper.Map<List<LoanDTO>>(loansList);
            return loansListDTO;
        }

        public async Task Delete (int id)
        {
            var loan = await _Repository.GetById(id);
            if (loan != null)
            {
                await _Repository.Delete(loan);
            }
        }

        public async Task UpdateDebt(string? userId, decimal amount, int? account)
        {
            var loan = await _Repository.GetById(account);
            loan.Debt += amount;
            await _Repository.Update(loan);
        }
    }
}


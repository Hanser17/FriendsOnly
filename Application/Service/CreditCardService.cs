using Application.DTOs.CreditCard;
using Application.DTOs.Loan;
using Application.Interfaces;
using Application.IRepository;
using Application.ViewModels.CreditCard;
using AutoMapper;
using Domain.Entities;


namespace Application.Service
{
    public class CreditCardService : GenericService<CreditCardSaveDTO, CreditCardDTO, CreditCard>, ICreditCardService
    {
        private readonly ICreditCardRepository _Repository;
        private readonly IMapper _mapper;
        public CreditCardService(ICreditCardRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _mapper = mapper;
            _Repository = repository;
        }


        public async Task UpdateDebt(string? userId, decimal amount, int? account)
        {
            var card = await _Repository.GetById(account);
            card.Debt += amount;
            await _Repository.Update(card);

        }
    }
}

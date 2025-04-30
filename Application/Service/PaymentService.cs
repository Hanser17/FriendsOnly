
using Application.DTOs.Payment;
using Application.Interfaces;
using Application.IRepository;
using AutoMapper;
using Domain.Entities;
using System.ComponentModel;

namespace Application.Service
{
    public class PaymentService :GenericService<PaymentSaveDTO, PaymentDTO, Payment>, IPaymentService
    {
        private readonly IPaymentRepository _Repository;
        private readonly IMapper _mapper;
        public PaymentService(IPaymentRepository Repository, IMapper mapper) : base(Repository, mapper)
        {
            _mapper = mapper;
            _Repository = Repository;
        }

        public async Task<decimal> GetDailyPayments()
        {
            return await _Repository.GetDailyPayments();
        }

        public async Task<decimal> GetTotalPayments()
        {
            return await _Repository.GetTotalPayments();
        }


        public async Task<PaymentSaveDTO> AddService(PaymentSaveDTO dto)
        {
            var payment = _mapper.Map<Payment>(dto);
            var paymentadded = await _Repository.Add(payment);

            return dto;
        }
    }
    
}

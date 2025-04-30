using Application.DTOs.Transfer;
using Application.Interfaces;
using Application.IRepository;
using AutoMapper;
using Domain.Entities;


namespace Application.Service
{
    public class TransferService : GenericService<TransferSaveDTO, TransferDTO, Transfer>, ITransferService
    {
        private readonly ITransferRepository _Repository;
        private readonly IMapper _mapper;
        public TransferService(ITransferRepository Repository, IMapper mapper) : base(Repository, mapper)
        {
            _Repository = Repository;
            _mapper = mapper;
        }
        public async Task<int> GetDailyTransactions()
        {
            return await _Repository.GetDailyTransactions();
        }

        public async Task<int> GetTotalTransactions()
        {
            return await _Repository.GetTotalTransactions();
        }
    }
}

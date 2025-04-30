using Application.DTOs.SavingsAccount;
using Application.Interfaces;
using Application.IRepository;
using AutoMapper;
using Domain.Entities;


namespace Application.Service
{
    public class SavingsAccountService : GenericService<SavingsAccountSaveDTO, SavingsAccountDTO, SavingsAccount>, ISavingsAccountService
    {
        private readonly ISavingsAccountRepository _Repository;
        private readonly IMapper _mapper;
        public SavingsAccountService(ISavingsAccountRepository Repository, IMapper mapper) :base(Repository, mapper)
        {
            _mapper = mapper;
            _Repository = Repository;
        }

        public async Task<SavingsAccountSaveDTO> AddAService(SavingsAccountSaveDTO dto)
        {
            var AccountExist = await _Repository.GetAccountByUserId(dto.UserId);
            if(AccountExist != null)
            {
                dto.IsPrimary = false;
            }else
            {
                dto.IsPrimary = true;
            }
                var saving = _mapper.Map<SavingsAccount>(dto);
            await _Repository.Add(saving);

            return dto;
        }
        public async Task DeleteService(int id)
        {
            var account = await _Repository.GetById(id);
            if (account != null)
            {
                await _Repository.Delete(account);
            }
        }

        public async Task<List<SavingsAccountDTO>> GetAllService(string userId)
        {
            var accountList = await _Repository.GetAll(userId);
            var accountListDTO = _mapper.Map<List<SavingsAccountDTO>>(accountList);
            return accountListDTO;
        }
        public async Task<SavingsAccountSaveDTO> GetbyUserId ( string userId)
        {
            var account = await _Repository.GetAccountByUserId(userId);
            return _mapper.Map<SavingsAccountSaveDTO>(account);

        }

        public async Task UpdateBalance(string? userId, decimal amount, int? id )
        {
          if(id != null)
            {
                var account = _Repository.GetById(id);
                account.Result.Balance += amount;
                await _Repository.Update(account.Result);
            }
            else
            {

                var account = _Repository.GetAccountByUserId(userId);
                account.Result.Balance += amount;
                await _Repository.Update(account.Result);

            }

        }
        public async Task<SavingsAccountSaveDTO> GetByIdSaveViewModelService(int id)
        {
            var data = await _Repository.GetById(id);
            return _mapper.Map<SavingsAccountSaveDTO>(data);
        }

        public async Task<SavingsAccountSaveDTO> UpdateService(SavingsAccountSaveDTO dto)
        {
            var saving = _mapper.Map<SavingsAccount>(dto);
            await _Repository.Update(saving);

            return dto;
        }
    }
}

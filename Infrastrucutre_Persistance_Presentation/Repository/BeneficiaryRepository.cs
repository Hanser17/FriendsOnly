using Application.DTOs.Beneficiary;
using Application.IRepository;
using Application.ViewModels.Beneficiary;
using AutoMapper;
using Domain.Entities;
using Identity.Context;
using Infrastrucutre_Persistance_Presentation.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastrucutre_Persistance_Presentation.Repository
{
    public class BeneficiaryRepository :GenericRepository<Beneficiary>, IBeneficiaryRepository
    {
        private readonly NetBankingContext _netBankingContext;
        private readonly IdentityContext _identityContext;
        private readonly IMapper _mapper;
       
        public BeneficiaryRepository(NetBankingContext netBankingContext, IMapper mapper, IdentityContext identityContext) : base(netBankingContext) 
        {
            _netBankingContext = netBankingContext;
            _identityContext = identityContext;
            _mapper = mapper;
        }

        public  async Task<List<BeneficiaryVM>> GetAllDto(string userId)
        {
            var beneficiaries = await _netBankingContext.Beneficiaries
            .Where(b => b.OwnerId == userId)
            .ToListAsync();

            var userIds = beneficiaries.Select(b => b.UserId).ToList();

            var users = await _identityContext.Users
                .Where(u => userIds.Contains(u.Id))
                .ToListAsync();

            var dataList = beneficiaries.Select(b => new BeneficiaryVM
            {
                Id = b.Id,
                AccountNumber = b.AccountNumber,
                UserId = b.UserId,
                FirstName = users.FirstOrDefault(u => u.Id == b.UserId)?.FirstName,
                LastName = users.FirstOrDefault(u => u.Id == b.UserId)?.LastName,
            }).ToList();

            return dataList;


        }
    }
}

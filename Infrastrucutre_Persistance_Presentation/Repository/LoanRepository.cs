using Application.IRepository;
using AutoMapper;
using Domain.Entities;
using Infrastrucutre_Persistance_Presentation.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastrucutre_Persistance_Presentation.Repository
{
    public class LoanRepository : GenericRepository<Loan>, ILoanRepository
    {
        private readonly NetBankingContext _netBankingContext;
        private readonly IMapper _mapper;
        public LoanRepository(NetBankingContext NetBankingContext) : base(NetBankingContext)
        {
            
        }
    }
}

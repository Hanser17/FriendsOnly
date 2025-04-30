using Application.IRepository;
using AutoMapper;
using Domain.Entities;
using Infrastrucutre_Persistance_Presentation.DBContext;


namespace Infrastrucutre_Persistance_Presentation.Repository
{
    public class CreditCardRepository : GenericRepository<CreditCard>, ICreditCardRepository
    {
        private readonly NetBankingContext _netBankingContext;
        private readonly IMapper _mapper;
        public CreditCardRepository(NetBankingContext friendsOnlyContext) : base(friendsOnlyContext)
        {

        }
    }
}

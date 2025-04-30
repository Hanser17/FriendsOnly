

using Application.IRepository;
using Domain.Entities;
using Infrastrucutre_Persistance_Presentation.DBContext;

namespace Infrastrucutre_Persistance_Presentation.Repository
{
    public class CashAdvanceRepository : GenericRepository<CashAdvance>, ICashAdvanceRepository
    {
        public CashAdvanceRepository(NetBankingContext NetBankingContext) : base(NetBankingContext)
        {

        }
    }
}

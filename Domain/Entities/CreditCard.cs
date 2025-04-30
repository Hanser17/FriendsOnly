

namespace Domain.Entities
{
    public class CreditCard : BaseEntity
    {
       
        public decimal CreditLimit { get; set; }
        public decimal Debt { get; set; }
    }
}

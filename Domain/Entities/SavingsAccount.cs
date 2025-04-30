

namespace Domain.Entities
{
    public class SavingsAccount : BaseEntity
    {
     
       
        public decimal Balance { get; set; }
        public bool IsPrimary { get; set; }
    }
}

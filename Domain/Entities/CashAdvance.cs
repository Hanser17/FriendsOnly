

namespace Domain.Entities
{
    public class CashAdvance : BaseEntity
    {
       
        public int CreditCardId { get; set; }
        public int DestinationAccountId { get; set; }
        public decimal Amount { get; set; }
        public decimal Interest { get; set; }
        public DateTime Date { get; set; }
    }
}



namespace Application.DTOs.CashAdvance
{
    public class CashAdvanceDTO
    {
        public int Id { get; set; }
        public int CreditCardId { get; set; }
        public int DestinationAccountId { get; set; }
        public decimal Amount { get; set; }
        public decimal Interest { get; set; }
        public DateTime Date { get; set; }
    }
}



namespace Application.DTOs.CashAdvance
{
    public class CashAdvanceSaveDTO
    {
        public int CreditCardId { get; set; }
        public int DestinationAccountId { get; set; }
        public decimal Amount { get; set; }
        public decimal Interest { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
    }
}



namespace Application.DTOs.CreditCard
{
    public class CreditCardSaveDTO
    {
        public string UserId { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal Debt { get; set; }
    }
}

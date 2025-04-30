

namespace Application.DTOs.CreditCard
{
    public class CreditCardDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal Debt { get; set; }
    }
}

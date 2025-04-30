

namespace Application.ViewModels.CreditCard
{
    public class CreditCardVM
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal Debt { get; set; }
    }
}

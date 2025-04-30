

using Application.DTOs.CreditCard;
using Application.DTOs.SavingsAccount;

namespace Application.ViewModels.Payments.CreditCardPayment
{
    public class CreditCardPaymentVM
    
    {
        public List<SavingsAccountDTO>? accounts { get; set; }

        public List<CreditCardDTO>? creditcards { get; set; }

        public decimal amount { get; set; }

        public int AccountNumber { get; set; }

        public int SelectedAccountId { get; set; }

    }
}

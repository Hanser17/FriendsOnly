using Application.DTOs.SavingsAccount;
using Application.ViewModels.SavingsAccount;


namespace Application.ViewModels.Payments.ExpressPayment
{
    public class ExpressPaymentVM
    {
        public List<SavingsAccountDTO>? accounts { get; set; }

        public  decimal amount { get; set; }

        public int AccountNumber { get; set; }

        public int SelectedAccountId { get; set; }

    }
}

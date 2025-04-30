using Application.DTOs.CreditCard;
using Application.DTOs.Loan;
using Application.DTOs.SavingsAccount;


namespace Application.ViewModels.User.Client
{
    public class ClientHomeVm
    {
        public List<SavingsAccountDTO>? SavingAccounts { get; set; }
        public List<CreditCardDTO>? CreditCards { get; set; }
        public List<LoanDTO>? Loans { get; set; }

    }
}


using Application.DTOs.Loan;
using Application.DTOs.SavingsAccount;


namespace Application.ViewModels.Payments.LoanPayment
{
    public class LoanPaymentVM
    {
        
            public List<SavingsAccountDTO>? accounts { get; set; }

            public List<LoanDTO>? loans { get; set; }

            public decimal amount { get; set; }

            public int AccountNumber { get; set; }

            public int SelectedAccountId { get; set; }

        
    }
}


using Application.DTOs.SavingsAccount;
using Application.ViewModels.Beneficiary;

namespace Application.ViewModels.Payments.BeneficiaryPayment
{
    
        public class BeneficiaryPayemtnVM
        {

            public List<SavingsAccountDTO>? accounts { get; set; }

            public List<BeneficiaryVM>? beneficiaries { get; set; }

            public decimal amount { get; set; }

            public int AccountNumber { get; set; }

            public int SelectedAccountId { get; set; }


        }
    
}

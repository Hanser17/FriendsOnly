

using Application.DTOs.SavingsAccount;

namespace Application.ViewModels.Transfer
{
    public class TransferVM
    {
        public List<SavingsAccountDTO>? from { get; set; }
        public List<SavingsAccountDTO>? To { get; set; }
        public int SourceAccountId { get; set; }
        public int DestinationAccountId { get; set; }
        public decimal Amount { get; set; }
    }
}

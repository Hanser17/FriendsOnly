

namespace Application.ViewModels.SavingAccount
{
    public class SavingsAccountSaveVM
    {
        public string UserId { get; set; }
        public decimal? Balance { get; set; }

        public bool IsPrimary { get; set; }
    }
}

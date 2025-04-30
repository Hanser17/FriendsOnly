

namespace Application.ViewModels.SavingsAccount
{
    public class SavingsAccountVM
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal Balance { get; set; }
        public bool IsPrimary { get; set; }
    }
}

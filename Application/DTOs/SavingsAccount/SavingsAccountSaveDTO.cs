

namespace Application.DTOs.SavingsAccount
{
    public class SavingsAccountSaveDTO
    {
        public string UserId { get; set; }
        public decimal? Balance { get; set; }
        public bool IsPrimary { get; set; }
    }
}


namespace Application.DTOs.Loan
{
    public class LoanDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public decimal Debt { get; set; }
    }
}

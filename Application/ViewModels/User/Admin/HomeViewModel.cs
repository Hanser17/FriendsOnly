

namespace Application.ViewModels.User.Admin
{
    public class HomeViewModel
    {
        public int TotalTransactions { get; set; }
        public int DailyTransactions { get; set; }
        public decimal TotalPayments { get; set; }
        public decimal DailyPayments { get; set; }
        public int ActiveCustomers { get; set; }
        public int InactiveCustomers { get; set; }
        public int TotalProducts { get; set; }
    }
}

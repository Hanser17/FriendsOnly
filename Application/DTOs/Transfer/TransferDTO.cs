

namespace Application.DTOs.Transfer
{
    public class TransferDTO
    {
        public int Id { get; set; }
        public int SourceAccountId { get; set; }
        public int DestinationAccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}

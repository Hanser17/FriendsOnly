

namespace Domain.Entities
{
    public class Payment : BaseEntity
    {

        public int SourceAccountId { get; set; }
        public int DestinationAccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}

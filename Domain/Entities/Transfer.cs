

namespace Domain.Entities
{
    public class Transfer :BaseEntity 
    {

       
        public int SourceAccountId { get; set; }
        public int DestinationAccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}

namespace Domain.Entities
{
    public class Beneficiary : BaseEntity
    {
       
        public int AccountNumber { get; set; }
        public string? OwnerId { get; set; }
    }
}

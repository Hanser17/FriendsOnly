

namespace Application.ViewModels.Beneficiary
{
    public class BeneficiaryVM
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string? OwnerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int AccountNumber { get; set; }
    }
}

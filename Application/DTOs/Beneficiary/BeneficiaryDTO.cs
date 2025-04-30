using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Beneficiary
{
    public class BeneficiaryDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string? OwnerId { get; set; }
        public string? UserName { get; set; }
        public int AccountNumber { get; set; }
    }
}

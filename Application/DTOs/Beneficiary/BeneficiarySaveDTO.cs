using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Beneficiary
{
    public class BeneficiarySaveDTO
    {
        public string UserId { get; set; }
        public int AccountNumber { get; set; }
        public string? OwnerId { get; set; }
    }
}

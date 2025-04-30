using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.UserDtos
{
    public class Authenticationrequest
    {
        public  string? Id { get; set; }
        public string UserName { get; set; }

        public string PassWord { get; set; }
    }
}

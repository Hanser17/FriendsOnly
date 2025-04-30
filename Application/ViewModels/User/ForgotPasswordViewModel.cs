using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.User
{
    public class ForgotPasswordViewModel
    {

        [Required(ErrorMessage = "UserName Required")]
        [DataType(DataType.Text)]
        public required string UserName { get; set; }

        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}

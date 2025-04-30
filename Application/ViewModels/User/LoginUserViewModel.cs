
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;


namespace Application.ViewModels.User
{
    public class LoginUserViewModel
    {
       [Required(ErrorMessage = "User name Required ")]
        [DataType(DataType.Text)]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Password name Required ")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        public bool HasError { get; set; }

        public string? Error { get; set; }
    }
}

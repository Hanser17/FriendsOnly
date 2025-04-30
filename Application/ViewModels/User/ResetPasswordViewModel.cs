
using System.ComponentModel.DataAnnotations;


namespace Application.ViewModels.User
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "UserName required")]
        [DataType(DataType.Text)]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "You most have a Token")]
        [DataType(DataType.Text)]
        public required string Token { get; set; }


        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password confirmation is required.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword{ get; set; }

        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}



using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.CreditCard
{
    public class CreditCardSaveVM
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "El balance es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El balance debe ser mayor que 0.")]
        public decimal CreditLimit { get; set; }
        public decimal Debt { get; set; }
    }
}

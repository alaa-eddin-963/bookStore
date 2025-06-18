using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class CheckoutRequestDto
    {
        [Required(ErrorMessage = "Address is required")]
        [MinLength(1)]
        public string ShippingAddress { get; set; } = string.Empty;
    }
}

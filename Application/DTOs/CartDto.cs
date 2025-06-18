using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class CartRequestDto
    {
        [Required(ErrorMessage = "Address is required")]
        [MinLength(1)]
        public string BookId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Quantity is requierd")]
        public int Quantity { get; set; } = 0;

    }
}

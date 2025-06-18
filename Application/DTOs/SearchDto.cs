using System.ComponentModel.DataAnnotations;
namespace DTOs
{
    public class SearchBookDto
    {
        [MaxLength(100)]
        public string? Title { get; set; }

        [MaxLength(100)]
        public string? Author { get; set; }

        [MaxLength(50)]
        public string? Genre { get; set; }
    }
}

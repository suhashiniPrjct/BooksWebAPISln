using BooksWebAPI.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace BooksWebAPI.Application.DTOs
{
    public class ProductCreateDTO
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string Isbn { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Author { get; set; } = null!;

        [Range(0.01, double.MaxValue, ErrorMessage = "ListPrice must be greater than 0")]
        public double ListPrice { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public double Price { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price50 must be greater than 0")]
        public double Price50 { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price100 must be greater than 0")]
        public double Price100 { get; set; }

        [Required(ErrorMessage = "CategoryId is required")]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(250)]
        public string? ImageUrl { get; set; } = null!;

    }
}

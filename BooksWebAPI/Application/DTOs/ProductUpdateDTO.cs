using BooksWebAPI.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace BooksWebAPI.Application.DTOs
{
    public class ProductUpdateDTO
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

        [Range(0.01, double.MaxValue)]
        public double ListPrice { get; set; }

        [Range(0.01, double.MaxValue)]
        public double Price { get; set; }

        [Range(0.01, double.MaxValue)]
        public double Price50 { get; set; }

        [Range(0.01, double.MaxValue)]
        public double Price100 { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(250)]
        public string? ImageUrl { get; set; } = null!;

    }
}

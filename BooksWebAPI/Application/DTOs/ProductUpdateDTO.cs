namespace BooksWebAPI.Application.DTOs
{
    public class ProductUpdateDTO
    {
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Isbn { get; set; } = null!;

        public string Author { get; set; } = null!;

        public double ListPrice { get; set; }

        public double Price { get; set; }

        public double Price50 { get; set; }

        public double Price100 { get; set; }
        public string ImageUrl { get; set; } = null!;
    }
}

using BooksWebAPI.Domain.Entities;
using BooksWebAPI.Application.DTOs;

namespace BooksWebAPI.Application.Mappers
{
    public static class ProductMapper
    {
        public static ProductReadDTO ToDTO(Product product)
        {
            return new ProductReadDTO
            {
                Id= product.Id,
                Description = product.Description,
                Title = product.Title,
                Isbn = product.Isbn,
                Author = product.Author,
                ListPrice = product.ListPrice,
                Price = product.Price,
                Price50 = product.Price50,
                Price100 = product.Price100,
                ImageUrl = product.ImageUrl
                //category collection do we need to add

            };
        }
        public static Product ToPrdt(ProductCreateDTO DTO)
        {
            return new Product
            {
                Description = DTO.Description,
                Title = DTO.Title,
                Isbn = DTO.Isbn,
                Author = DTO.Author,
                ListPrice = DTO.ListPrice,
                Price = DTO.Price,
                Price50 = DTO.Price50,
                Price100 = DTO.Price100,
                ImageUrl = DTO.ImageUrl

            };
        }
    }
}

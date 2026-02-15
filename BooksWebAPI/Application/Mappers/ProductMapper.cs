using BooksWebAPI.Application.DTOs;
using BooksWebAPI.Domain.Entities;
using Humanizer;

namespace BooksWebAPI.Application.Mappers
{
    public static class ProductMapper
    {
        //read 
        public static ProductReadDTO ToDTO(Product product)
        {
            //Entity from db -> DTO 
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
                CategoryId = product.CategoryId,
                Category = product.Category?.Name ?? " ",
                ImageUrl = product.ImageUrl ?? "/images/default.png"
                //category collection do we need to add

            };
        }
        // Created DTO → Entity create
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
                CategoryId=DTO.CategoryId,//this is value is assigned by selectlistitem 'value' whose value are dynamically assigned by getting category list in update/create operation
                // no need of handling category navigation EF will handle it
                ImageUrl = DTO.ImageUrl ?? "/images/default.png"

            };
        }
        //updated DTO ->entity- update
        public static void MapUpdate(ProductUpdateDTO DTO, Product product)
        {

            DTO.Description = product.Description;
            DTO.Title = product.Title;
            DTO.Isbn = product.Isbn;
            DTO.Author = product.Author;
            DTO.ListPrice = product.ListPrice;
            DTO.Price = product.Price;
            DTO.Price50 = product.Price50;
            DTO.Price100 = product.Price100;
            DTO.CategoryId = product.CategoryId;
            //DTO.Category = product.Category?.Name ?? " ",
            DTO.ImageUrl = product.ImageUrl ?? product.ImageUrl ?? "/images/default.png";

        }
    }

}

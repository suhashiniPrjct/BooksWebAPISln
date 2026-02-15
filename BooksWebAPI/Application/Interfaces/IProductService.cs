using BooksWebAPI.Application.DTOs;

namespace BooksWebAPI.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductReadDTO>> GetallAsync();
        Task<ProductReadDTO> GetByIdAsync(int id);
        Task<ProductReadDTO> CreateAsync(ProductCreateDTO productCreateDTO);
        Task<bool> UpdateAsync(int id, ProductUpdateDTO productCreateDTO);
        Task<bool> DeleteAsync(int id);
    }
}

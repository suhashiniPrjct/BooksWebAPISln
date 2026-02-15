using BooksWebAPI.Application.DTOs;
using BooksWebAPI.Application.Interfaces;
using BooksWebAPI.Application.Mappers;
using BooksWebAPI.Data;
using BooksWebAPI.Domain.Entities;
using Humanizer;
using Microsoft.EntityFrameworkCore;
//using AutoMapper;

namespace BooksWebAPI.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        string defaultImageUrl = "/images/default.png";
        
        public ProductService(AppDbContext context)//, IMapper mapper)
        {
            _context = context;
            

        }
        public async Task<IEnumerable<ProductReadDTO>> GetallAsync()
        {
           //
           return await _context.Products
                        .AsNoTracking()
                        .Include(s=>s.Category)
                        .Select(p=>ProductMapper.ToDTO(p)).ToListAsync();
                
        }
        public async Task<ProductReadDTO?> GetByIdAsync(int id)
        {
            var product=await _context.Products
                                       .Include(s=>s.Category)
                                       .FirstOrDefaultAsync(p=>p.Id == id);

            return product == null ? null : ProductMapper.ToDTO(product);
        }
        public async Task<bool> UpdateAsync(int id,ProductUpdateDTO updatedDTO)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            return false;
            if (updatedDTO.Price50 > updatedDTO.Price50) {
                throw new ArgumentException("Price50 cannot be greater than Price100");
            }
            var CategoryExists=await _context.Categories.AnyAsync(c=>c.Id == updatedDTO.CategoryId);
            if (!CategoryExists)
            {
                throw new ArgumentException("Category does not exist");
            }
            
            ProductMapper.MapUpdate(updatedDTO,product);
            await _context.SaveChangesAsync();
            return true;
            
        }
        public async Task<ProductReadDTO> CreateAsync(ProductCreateDTO CDTO)
        {
            // Business validations (multi-field / DB dependent)
            if (CDTO.Price50 > CDTO.Price100)
                throw new ArgumentException("Price50 cannot be greater than Price100");

            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == CDTO.CategoryId);
            if (!categoryExists)
                throw new ArgumentException("Category does not exist");
           

            var product = ProductMapper.ToPrdt(CDTO);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return ProductMapper.ToDTO(product);

        }

        public async Task<bool> DeleteAsync(int id)
        {
             var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return false; // Controller handles 404

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;

        }


    }
}

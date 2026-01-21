using BooksWebAPI.Application.DTOs;
using BooksWebAPI.Application.Interfaces;
using BooksWebAPI.Application.Mappers;
using BooksWebAPI.Data;
using Microsoft.EntityFrameworkCore;
//using AutoMapper;

namespace BooksWebAPI.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        
        public ProductService(AppDbContext context)//, IMapper mapper)
        {
            _context = context;
            

        }
        public async Task<IEnumerable<ProductReadDTO>> GetallAsyn()
        {
           //
           return await _context.Products
                        .AsNoTracking()
                        .Include(s=>s.Category)
                        .Select(p=>ProductMapper.ToDTO(p)).ToListAsync();
                
        }
        public async Task<ProductReadDTO> GetByIdAsync(int id)
        {
            var product=await _context.Products
                                       .Include(s=>s.Category)
                                       .FirstOrDefaultAsync(p=>p.Id == id);

            return product == null ? null : ProductMapper.ToDTO(product);
        }
        public async Task<bool> UpdateAsync(int id,ProductCreateDTO productCreateDTO)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return false;
            
            ProductMapper.ToPrdt(productCreateDTO);
            await _context.SaveChangesAsync();
            return true;
            
        }
        public async Task<ProductReadDTO> CreateAsync(ProductCreateDTO productCreateDTO)
        {
            var product = ProductMapper.ToPrdt(productCreateDTO);
            _context.Add(product);
            await _context.SaveChangesAsync();
            return ProductMapper.ToDTO(product);
            
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id==id);

            if(product == null)

                    return false;
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
            
        }


    }
}

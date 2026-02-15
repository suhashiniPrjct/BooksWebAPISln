using BooksWebAPI.Application.DTOs;
using BooksWebAPI.Application.Interfaces;
using BooksWebAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BooksWebAPI.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _pservice;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductService pservice, ILogger<ProductController> logger)
        {
            _pservice = pservice;
            _logger = logger;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //try
            //{
                _logger.LogInformation("Getting all products");
                var products = await _pservice.GetallAsync();
                return Ok(products);
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, "Unexpected error fetching all products");
            //    return BadRequest(ex);
            //}

            
        }

        // GET api/Product/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<IActionResult?> GetById(int id)
        {
            //Unhandled exceptions handled at Custom Middleware
            //try
            //{

                _logger.LogInformation("Fetching product with id: {id}", id);                               
                var product = await _pservice.GetByIdAsync(id);
                if(product == null)
                {
                    _logger.LogWarning("Product not found for the id {id}",id);
                    return NotFound("Product not found");
                }
                else
                {
                    return Ok(product);
                }                  
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, "Unexpected error  while fetching portfolio for clientId {ClientId}", id);

            //    return StatusCode(500,"An error occured");
            //}

        }

        // POST api/Product
        [HttpPost]
        [Authorize("Admin")]
        public async Task<IActionResult> Create([FromBody] ProductCreateDTO PCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("Invalid Product creation");
                return BadRequest(ModelState);
            }
            else
            {
                var newProdDTO = await _pservice.CreateAsync(PCreateDTO);
                _logger.LogInformation("new product is created with id: {id}", newProdDTO.Id);
                return CreatedAtAction(nameof(GetById), new { id = newProdDTO.Id }, newProdDTO);
            }
        }

        // PUT api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateDTO PUpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Unsuccesful attempt of updating the product id: {id}", id);
                return BadRequest(ModelState);
            }
            var Updatedprod = await _pservice.UpdateAsync(id, PUpdateDTO);
            if(!Updatedprod)
            {
                _logger.LogWarning("Product not found for update with id: {Id}", id);
                return NotFound("Product not found");
            }
            return NoContent();
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        [Authorize("Admin")]
        public async Task<IActionResult> Delete(int id)
        {

            var deleted=await _pservice.DeleteAsync(id);
            if (!deleted)
            {
                _logger.LogWarning("Product not found for deletion with id: {Id}", id);
                return NotFound("Product not found"); 
            }
            _logger.LogWarning("Deleted the product with id: {Id}", id);
            return NoContent();
        }
    }
}

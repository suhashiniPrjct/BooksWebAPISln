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

        //private readonly AppDbContext _context;
        private readonly IProductService _pservice;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductService pservice, ILogger<ProductController> logger)
        {
            _pservice = pservice;
            _logger = logger;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Get all products");
            return Ok(await _pservice.GetallAsyn());
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var product = await _pservice.GetByIdAsync(id);

                return product == null ? NotFound() : Ok(product);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching portfolio for clientId {ClientId}", id);

                return StatusCode(500,"An error occured");
            }

        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateDTO Cdto)
        {
            var newProdDTO= await _pservice.CreateAsync(Cdto);
            return CreatedAtAction(nameof(GetById),new {id=newProdDTO.Id},newProdDTO);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateDTO Updatdto)
        {
            var Updatedprod=await _pservice.UpdateAsync(id,Updatdto);
            return Updatedprod ? NoContent() : NotFound();
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted=await _pservice.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}

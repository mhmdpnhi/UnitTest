using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Entities;
using WebApi.Models.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_productService.GetAll());
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var pr = _productService.GetById(id);

            if (pr is null)
                return NotFound();
            else
                return Ok(pr);
        }

        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Post([FromBody] Product value)
        {
            if (ModelState.IsValid is not true)
            {
                return BadRequest(ModelState);
            }

            var result = _productService.Create(value);
            if (result is true)
            {
                var pr = _productService.GetById(value.ID);

                return CreatedAtAction("Get", new { id = pr.ID }, pr);
            }
            else 
                return BadRequest(result);
        }

        // PUT api/<ProductController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _productService.Delete(id);
            if (result is true)
                return Ok();
            else
                return NotFound();
        }
    }
}

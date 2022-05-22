using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using DataAccess.Repository;

namespace eStoreAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/Product
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string? queryKeyword)
        {
            var list =  await _productRepository.GetMany(queryKeyword);
            if (list == null)
            {
                return NotFound();
            }

            return Ok(list);
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Member))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            // if (_context.Products == null)
            // {
            //     return NotFound();
            // }
            var product = await _productRepository.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            try
            {
                await _productRepository.Update(product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _productRepository.Get(id) == null)
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            // if (_context.Products == null)
            // {
            //     return Problem("Entity set 'FStoreDBContext.Products'  is null.");
            // }
            try
            {
                await _productRepository.Add(product);
            }
            catch (DbUpdateException)
            {
                if (await _productRepository.Get(product.ProductId) != null)
                {
                    return Conflict();
                }

                throw;
            }

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            // if (_context.Products == null)
            // {
            //     return NotFound();
            // }

            var product = await _productRepository.Get(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productRepository.Delete(id);
            return NoContent();
        }

        // private bool ProductExists(int id)
        // {
        //     return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        // }
    }
}
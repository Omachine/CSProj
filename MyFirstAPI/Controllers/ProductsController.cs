using Microsoft.AspNetCore.Mvc;
using MyFirstApi.Models;

namespace MyFirstApi.Controllers

{

    [ApiController]
    [Route("api/[controller]")]

    public class ProductsController : ControllerBase
    {
        private static readonly List<Product> Products =
        [
            new Product { Id = 1, Name = "Apple", Price = 1.25m },
            new Product { Id = 2, Name = "Banana", Price = 0.75m },
            new Product { Id = 3, Name = "Orange", Price = 1.55m }
        ];

        [HttpGet]
        public ActionResult<List<string>> Get()

        {

            return Ok(Products);

        }


       [HttpGet("{id:int}")]
        public ActionResult<List<Product>> GetById(int id)
        {
            var product = Products.FirstOrDefault(product => product.Id == id);

            return product is null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public ActionResult<Product> Post(Product product)
        {
            product.Id = Products.Max(product => product.Id) + 1;
            Products.Add(product);

            return CreatedAtAction(nameof(GetById), new {id = product.Id}, product);
        }
       
       [HttpPut("{id:int}")]
       public ActionResult<Product> Put(int id, Product updatedProduct)
        {
            var product = Products.FirstOrDefault(product => product.Id == id);

            if (product is null)
            {
                return NotFound();
            }

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;

            return Ok(product);
        }


        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var product = Products.FirstOrDefault(product => product.Id == id);

            if (product is null)
            {
                return NotFound();
            }

            Products.Remove(product);
            return NoContent();
        }
    }

}
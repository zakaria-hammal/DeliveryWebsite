using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class StockController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProducts()
        {
            using var db = new Delivery();

            List<Product> products = db.Products.ToList();

            return Ok(products);
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            using var db = new Delivery();

            List<Category> categories = db.Categories.ToList();

            return Ok(categories);
        }

        [HttpPost]
        public IActionResult PostProduct([FromBody] Product product)
        {
            using var db = new Delivery();

            db.Products.Add(product);
            db.SaveChanges();

            return Ok("Product Added Successfully");
        }

        [HttpPost]
        public IActionResult PostCategory([FromBody] Category category)
        {
            using var db = new Delivery();

            db.Categories.Add(category);
            db.SaveChanges();

            return Ok("Category Added Successfully");
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            using var db = new Delivery();

            Product? product = db.Products.FirstOrDefault(p => p.ProductID == id);

            if(product == null)
                return NotFound("Product Not Found");

            db.Remove(product);
            db.SaveChanges();
            var imagePath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(),"Images"), product.ProductName + ".png");

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            return Ok("Product Deleted Successfully");
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            using var db = new Delivery();

            Category? category = db.Categories.FirstOrDefault(c => c.CategoryID == id);

            if(category == null)
                return NotFound("Category Not Found");

            var products = db.Products.Where(p => p.CategoryName == category.CategoryName);

            foreach (var product in products)
            {
                db.Remove(product);
            }

            db.Remove(category);

            db.SaveChanges();

            return Ok("Category Deleted Successfully");
        }
    }
}
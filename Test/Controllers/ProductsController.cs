using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.Data;
using Test.Models;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public ProductsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [Route("GetByCategory/{Id}")]
        public async Task<ActionResult<List<Product>>> GetByCategory(int? Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }

            if (!db.Categories.Where(x=>x.Id == Id).Any())
            {
                return BadRequest();
            }

            var data  = await db.CategoryProducts
                .Include(x=>x.Product)
                .Where(x => x.CategoryId == Id)
                .Select(x => x.Product)
                .ToListAsync();

            if (data == null)
            {
                return NoContent();
            }

            return Ok(data);
        }

        [Authorize]
        [HttpPost]
        [Route("Post")]
        public async Task<ActionResult> Post([FromBody] Product model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            await db.Products.AddAsync(model);
            await db.SaveChangesAsync();

            return Ok();
        }


        [Authorize]
        [HttpPut]
        [Route("Put")]
        public async Task<IActionResult> Put([FromBody] Product model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var searchModel = await db.Products.FindAsync(model.Id);
            if (searchModel == null)
            {
                return BadRequest();
            }


            searchModel.Name = model.Name;
            db.Entry(searchModel).State = EntityState.Modified;

            await db.SaveChangesAsync();

            return Ok();
        }


        [Authorize]
        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<ActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }

            var data = await db.Products.FindAsync(Id);
            if (data == null)
            {
                return BadRequest();
            }

            db.Remove(data);
            await db.SaveChangesAsync();

            return Ok();

        }
    }
}

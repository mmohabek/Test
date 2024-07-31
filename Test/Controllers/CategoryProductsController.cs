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
    public class CategoryProductsController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public CategoryProductsController(ApplicationDbContext db)
        {
            this.db = db;
        }


        [Authorize]
        [HttpPost]
        [Route("Post")]
        public async Task<ActionResult> Post([FromBody] CategoryProduct model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            await db.CategoryProducts.AddAsync(model);
            await db.SaveChangesAsync();

            return Ok();
        }


        [Authorize]
        [HttpDelete]
        [Route("Delete/{CategoryId}/{ProductId}")]
        public async Task<ActionResult> Delete(int? CategoryId, int? ProductId)
        {
            if (CategoryId == null || ProductId == null)
            {
                return BadRequest();
            }

            var Q = db.CategoryProducts
                .Where(x=>x.ProductId == ProductId
                && x.CategoryId == CategoryId
                ).AsQueryable();

            if (Q.Count() ==0)
            {
                return BadRequest();
            }

            var data = await Q.FirstOrDefaultAsync();

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

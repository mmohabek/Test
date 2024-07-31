using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.Data;
using Test.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public CategoriesController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<Category>>> GetAll()
        {

            var c =  await db.Categories.ToListAsync();
            if (c == null)
            {
                return NoContent();
            }

            return Ok(c);
        }


        [Authorize]
        [HttpPost]
        [Route("Post")]
        public async Task<ActionResult> Post([FromBody] Category model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            await db.Categories.AddAsync(model);
            await db.SaveChangesAsync();

            return Ok();
        }


        [Authorize]
        [HttpPut]
        [Route("Put")]
        public async Task<IActionResult> Put([FromBody] Category model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var  searchModel = await db.Categories.FindAsync(model.Id);
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

            var data = await db.Categories.FindAsync(Id);
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

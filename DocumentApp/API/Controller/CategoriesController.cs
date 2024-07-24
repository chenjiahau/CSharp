using Microsoft.AspNetCore.Mvc;
using Domain;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetCategories()
        {
            return await Mediator.Send(new Application.Categories.List.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(Guid id)
        {
            return await Mediator.Send(new Application.Categories.Detail.Query { Id = id });
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            await Mediator.Send(new Application.Categories.Create.Command { Category = category });
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, Category category)
        {
            category.Id = id;
            await Mediator.Send(new Application.Categories.Update.Command { Category = category });
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await Mediator.Send(new Application.Categories.Delete.Command { Id = id });
            return Ok();
        }
    }
}
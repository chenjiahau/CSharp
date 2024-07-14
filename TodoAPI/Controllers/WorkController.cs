using Microsoft.AspNetCore.Mvc;
using TodoAPI.Data;
using TodoAPI.Models;
using TodoAPI.Models.DTOs;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkController : ControllerBase
    {
        private readonly TodoDbContext dbContext;

        public WorkController(TodoDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        /**
         * URL: /api/Work/{id}
         * METHOD: GET
         */
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            // Fetch from database
            var workModel = dbContext.Works.FirstOrDefault(x => x.Id == id);

            if (workModel == null)
            {
                return NotFound();
            }

            // Convert Work model to Work DTO
            var workDto = new WorkDTO
            {
                Id = workModel.Id,
                Title = workModel.Title,
                Description = workModel.Description,
            };

            // Return Work DTO
            return Ok(workDto);
        }

        /**
         * URL: /api/Work
         * METHOD: POST
         */
        [HttpPost]
        public IActionResult Add([FromBody] AddWorkDTO addWorkDTO)
        {
            // Convert Work DTO to Work model
            var workModel = new Work
            {
                Title = addWorkDTO.Title,
                Description = addWorkDTO.Description
            };

            // Insert to database
            dbContext.Works.Add(workModel);
            dbContext.SaveChanges();

            // Convert Work model to Work DTO
            var workDTO = new WorkDTO
            {
                Id = workModel.Id,
                Title = workModel.Title,
                Description = workModel.Description
            };

            // Return Work DTO
            return Ok(workDTO);
        }

        /**
         * URL: /api/Work/{id}
         * METHOD: PUT
         */
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult PutById([FromRoute] Guid id, [FromBody] UpdateWorkDTO updateWorkDTO)
        {
            // Fetch from database
            var workModel = dbContext.Works.FirstOrDefault(x => x.Id == id);

            if (workModel == null)
            {
                return NotFound();
            }

            // Update Work model
            workModel.Title = updateWorkDTO.Title;
            workModel.Description = updateWorkDTO.Description;

            // Save to database
            dbContext.SaveChanges();

            // Convert Work model to Work DTO
            var workDTO = new WorkDTO
            {
                Id = workModel.Id,
                Title = workModel.Title,
                Description = workModel.Description
            };

            // Return Work DTO
            return Ok(workDTO);
        }

        /**
         * URL: /api/Work/{id}
         * METHOD: DELETE
         */
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult DeleteById([FromRoute] Guid id)
        {
            // Fetch from database
            var workModel = dbContext.Works.FirstOrDefault(x => x.Id == id);

            if (workModel == null)
            {
                return NotFound();
            }

            // Save to database
            dbContext.Works.Remove(workModel);
            dbContext.SaveChanges();

            // Convert Work model to Work DTO
            var workDTO = new WorkDTO
            {
                Id = workModel.Id,
                Title = workModel.Title,
                Description = workModel.Description
            };

            // Return Work DTO
            return Ok(workDTO);
        }
    }
}


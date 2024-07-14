using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        private readonly IMapper mapper;

        public WorkController(TodoDbContext _dbContext, IMapper _mapper)
        {
            dbContext = _dbContext;
            mapper = _mapper;
        }

        /**
         * URL: /api/Work/{id}
         * METHOD: GET
         */
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            // Fetch from database
            var workModel = await dbContext.Works.FirstOrDefaultAsync(x => x.Id == id);

            if (workModel == null)
            {
                return NotFound();
            }

            // Convert Work model to Work DTO
            var workDto = mapper.Map<WorkDTO>(workModel);

            // Return Work DTO
            return Ok(workDto);
        }

        /**
         * URL: /api/Work
         * METHOD: POST
         */
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddWorkDTO addWorkDTO)
        {
            // Convert Work DTO to Work model
            var workModel = new Work
            {
                Title = addWorkDTO.Title,
                Description = addWorkDTO.Description
            };

            // Insert to database
            await dbContext.Works.AddAsync(workModel);
            await dbContext.SaveChangesAsync();

            // Convert Work model to Work DTO
            var workDTO = mapper.Map<WorkDTO>(workModel);

            // Return Work DTO
            return Ok(workDTO);
        }

        /**
         * URL: /api/Work/{id}
         * METHOD: PUT
         */
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> PutById([FromRoute] Guid id, [FromBody] UpdateWorkDTO updateWorkDTO)
        {
            // Fetch from database
            var workModel = await dbContext.Works.FirstOrDefaultAsync(x => x.Id == id);

            if (workModel == null)
            {
                return NotFound();
            }

            // Update Work model
            workModel.Title = updateWorkDTO.Title;
            workModel.Description = updateWorkDTO.Description;

            // Save to database
            await dbContext.SaveChangesAsync();

            // Convert Work model to Work DTO
            var workDTO = mapper.Map<WorkDTO>(workModel);

            // Return Work DTO
            return Ok(workDTO);
        }

        /**
         * URL: /api/Work/{id}
         * METHOD: DELETE
         */
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            // Fetch from database
            var workModel = await dbContext.Works.FirstOrDefaultAsync(x => x.Id == id);

            if (workModel == null)
            {
                return NotFound();
            }

            // Save to database
            dbContext.Works.Remove(workModel);
            await dbContext.SaveChangesAsync();

            // Convert Work model to Work DTO
            var workDTO = mapper.Map<WorkDTO>(workModel);

            // Return Work DTO
            return Ok(workDTO);
        }
    }
}


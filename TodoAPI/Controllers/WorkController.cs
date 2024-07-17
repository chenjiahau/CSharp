using System.Data;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TodoAPI.Models;
using TodoAPI.Models.DTOs;
using TodoAPI.Repositories;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkController : ControllerBase
    {
        private readonly IWorkRepository workRepository;
        private readonly IMapper mapper;

        public WorkController(IWorkRepository _workRepository, IMapper _mapper)
        {
            workRepository = _workRepository;
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
            var workModel = await workRepository.GetById(id);

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
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> Add([FromBody] AddWorkDTO addWorkDTO)
        {
            // Convert Work DTO to Work model
            var workModel = new Work
            {
                Title = addWorkDTO.Title,
                Description = addWorkDTO.Description
            };

            // Insert to database
            workModel = await workRepository.Add(workModel);

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
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> PutById([FromRoute] Guid id, [FromBody] UpdateWorkDTO updateWorkDTO)
        {
            // Save to database
            var workModel = new Work
            {
                Title = updateWorkDTO.Title,
                Description = updateWorkDTO.Description
            };

            workModel = await workRepository.PutById(id, workModel);

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
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            // Save to database
            var workModel = await workRepository.DeleteById(id);

            // Convert Work model to Work DTO
            var workDTO = mapper.Map<WorkDTO>(workModel);

            // Return Work DTO
            return Ok(workDTO);
        }
    }
}
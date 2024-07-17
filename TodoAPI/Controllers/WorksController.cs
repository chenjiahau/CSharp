using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TodoAPI.Models.DTOs;
using TodoAPI.Repositories;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorksController : ControllerBase
    {
        private readonly IWorkRepository workRepository;
        private readonly IMapper mapper;

        public WorksController(IWorkRepository _workRepository, IMapper _mapper)
        {
            workRepository = _workRepository;
            mapper = _mapper;
        }

        /**
         * URL: /api/Works
         * METHOD: GET
         */
        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? column,
            [FromQuery] string? keyword,
            [FromQuery] string? sortBy,
            [FromQuery] bool isAsc,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 1
        ) {
            // Fetch from database
            var workModels = await workRepository.GetAll(column, keyword, sortBy, isAsc, pageNumber, pageSize);

            // Convert Work models to Work DTOs
            var workDTOs = mapper.Map<List<WorkDTO>>(workModels);

            // Return Work DTOs
            return Ok(workDTOs);
        }
    }
}
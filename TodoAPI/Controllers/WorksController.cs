using AutoMapper;
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
        public async Task<IActionResult> GetAll()
        {
            // Fetch from database
            var workModels = await workRepository.GetAll();

            // Convert Work models to Work DTOs
            var workDTOs = mapper.Map<List<WorkDTO>>(workModels);

            // Return Work DTOs
            return Ok(workDTOs);
        }
    }
}
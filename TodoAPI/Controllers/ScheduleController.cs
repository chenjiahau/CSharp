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
    public class ScheduleController : Controller
    {
        private readonly IScheduleRepository scheduleRepository;
        private readonly IMapper mapper;

        public ScheduleController(IScheduleRepository _scheduleRepository, IMapper _mapper)
        {
            scheduleRepository = _scheduleRepository;
            mapper = _mapper;
        }

        /**
         * URL: /api/Schedule/{id}
         * METHOD: GET
         */
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById(Guid id)
        {
            // Fetch from database
            var scheduleModel = await scheduleRepository.GetById(id);

            if (scheduleModel == null)
            {
                return NotFound();
            }

            // Convert Schedule model to Schedule DTO
            var scheduleDTO = mapper.Map<ScheduleDTO>(scheduleModel);

            // Return Schedule DTO
            return Ok(scheduleDTO);
        }

        /**
         * URL: /api/Schedule
         * METHOD: POST
         */
        [HttpPost]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> Add([FromBody] AddScheduleDTO addScheduleDTO)
        {
            // Convert Schedule DTO to Schedule model
            var scheduleModel = new Schedule
            {
                Title = addScheduleDTO.Title,
                ExectionDate = addScheduleDTO.ExectionDate,
                IsActived = addScheduleDTO.IsActived,
                UserId = addScheduleDTO.UserId,
                WorkId = addScheduleDTO.WorkId
            };

            // Insert to database
            scheduleModel = await scheduleRepository.Add(scheduleModel);
            var scheduleDTO = new ScheduleDTO { };
            if (scheduleModel != null)
            {
                scheduleDTO = mapper.Map<ScheduleDTO>(scheduleModel);
            }

            // Return Schedule DTO
            return Ok(scheduleDTO);
        }

        /**
         * URL: /api/Schedule/{id}
         * METHOD: PUT
         */
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> PutById([FromRoute] Guid id, [FromBody] UpdateScheduleDTO updateScheduleDTO)
        {
            // Save to database
            var scheduleModel = new Schedule
            {
                Title = updateScheduleDTO.Title,
                ExectionDate = updateScheduleDTO.ExectionDate,
                IsActived = updateScheduleDTO.IsActived,
                UserId = updateScheduleDTO.UserId,
                WorkId = updateScheduleDTO.WorkId
            };

            await scheduleRepository.PutById(id, scheduleModel);

            // Convert Schedule model to Schedule DTO
            var newScheduleModel = await scheduleRepository.GetById(id);
            var scheduleDTO = mapper.Map<ScheduleDTO>(newScheduleModel);

            // Return Schedule DTO
            return Ok(scheduleDTO);
        }

        /**
         * URL: /api/Schedule/{id}
         * METHOD: DELETE
         */
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            // Save to database
            var scheduleModel = await scheduleRepository.DeleteById(id);

            if (scheduleModel == null)
            {
                return NotFound();
            }

            // Convert Schedule model to Schedule DTO
            var scheduleDTO = mapper.Map<ScheduleDTO>(scheduleModel);

            // Return Schedule DTO
            return Ok(scheduleDTO);
        }
    }
}
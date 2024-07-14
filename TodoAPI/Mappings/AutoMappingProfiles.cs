using AutoMapper;

using TodoAPI.Models;
using TodoAPI.Models.DTOs;

namespace TodoAPI.Mappings
{
	public class AutoMappingProfile : Profile
	{
		public AutoMappingProfile()
		{
			CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Work, WorkDTO>().ReverseMap();
            CreateMap<Schedule, ScheduleDTO>().ReverseMap();
        }
	}
}


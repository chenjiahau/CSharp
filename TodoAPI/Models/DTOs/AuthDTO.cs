using System.ComponentModel.DataAnnotations;

namespace TodoAPI.Models.DTOs
{
	public class AuthDTO
	{
		[Required]
		[DataType(DataType.EmailAddress)]
		public string? Username { get; set; }

		[Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

		public string[] Roles { get; set; }
    }
}


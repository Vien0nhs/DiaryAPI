using System.ComponentModel.DataAnnotations;

namespace DiaryAPI.Models
{
	public class User
	{
		[Key]
		public int UserID { get; set; }
		[Required]
		[MaxLength(50)]
		public string Username { get; set; }
		[Required]
		public string Password { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set; }

	}
}

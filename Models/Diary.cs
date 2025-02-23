using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiaryAPI.Models
{
	public class Diary
	{
		[Key]
		public int DiaryID { get; set; }
		[Required]
		public int UserID { get; set; }
		[Required]
		[MaxLength(300)]
		public string Title { get; set; }
		[Required]
		public string Content { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set; }
		[ForeignKey(nameof(UserID))]
		public User User { get; set; }
	}
}

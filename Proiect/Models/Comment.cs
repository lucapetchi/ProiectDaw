using Proiect.Models.Base;

namespace Proiect.Models
{
	public class Comment : BaseEntity
	{
		public string? Content { get; set; }
		public DateTime Date { get; set; }

		public Guid Taskid { get; set; }
		public Task? task { get; set; }
	}
}

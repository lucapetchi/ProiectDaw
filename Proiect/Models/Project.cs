using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Proiect.Models.Base;

namespace Proiect.Models
{
	public class Project: BaseEntity
	{
		

		[Column(TypeName = "nvarchar(50)")]
		[Required(ErrorMessage = "Numele proiectului este obligatoriu")]
		public string? Name { get; set; }

		[Column(TypeName = "nvarchar(200)")]
		[Required(ErrorMessage = "Descrierea este obligatorie")]
		public string? Description { get; set; }
		public Guid? organizer_id { get; set; }
		public virtual ICollection<Task> Tasks { get; set; }
		public virtual ICollection<UserProject> Users { get; set; }

	}
}

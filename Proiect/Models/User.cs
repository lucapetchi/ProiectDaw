using Proiect.Models.Base;

namespace Proiect.Models
{
    public class User : BaseEntity
    {
		public virtual ICollection<Task>? Tasks { get; set; }
		public virtual ICollection<User>? Projects { get; set; }

	}
}

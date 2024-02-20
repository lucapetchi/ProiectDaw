namespace Proiect.Models
{
    public class User
    {
		public virtual ICollection<Task>? Tasks { get; set; }
		public virtual ICollection<User>? Projects { get; set; }

	}
}

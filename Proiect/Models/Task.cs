namespace Proiect.Models
{
    public class Task
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Status { get; set; }
        public DateTime Deadline { get; set; }

		public int? ProjectId { get; set; }

		public string? UserId { get; set; }
		public virtual Project? Project { get; set; }
		public virtual User? User { get; set; }
	}
}

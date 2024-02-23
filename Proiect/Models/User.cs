using Proiect.Models.Base;

namespace Proiect.Models
{
	public class User : BaseEntity
	{

		public string? UserName { get; set; }
		public byte[] PasswordHash { get; set; }
		public byte[] PasswordSalt { get; set; }

		public string Token { get; set; } = string.Empty;
		public DateTime TokenCreated { get; set; } = DateTime.Now;
		public DateTime TokenExpires { get; set; }
		public virtual ICollection<UserProject>? Projects { get; set; }

	}
}

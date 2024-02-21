using Microsoft.EntityFrameworkCore;
using Proiect.Models;
using System.Security.Claims;

namespace Proiect.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }
		public DbSet<Comment> Comments { get; set; }

		public DbSet<User> Users { get; set; }
		public DbSet<UserProject> UserProjects { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//one-to-one 

			modelBuilder.Entity<Models.Task>()
				   .HasOne(t => t.comment)
				   .WithOne(c => c.task)
				   .HasForeignKey<Comment>(c => c.Taskid);

			//one-to-many 

			modelBuilder.Entity<Project>()
				.HasMany(t => t.Tasks)
				.WithOne(p => p.Project)
				.HasForeignKey(t => t.ProjectId);


			base.OnModelCreating(modelBuilder);

			
;

			//one-to-many for many-to-many
			modelBuilder.Entity<UserProject>()
					.HasOne(usp => usp.User)
					.WithMany(m => m.Projects)
					.HasForeignKey(usp => usp.UserId);

			modelBuilder.Entity<UserProject>()
				   .HasOne(usp => usp.Project)
				   .WithMany(e => e.Users)
				   .HasForeignKey(usp => usp.ProjectId);

			base.OnModelCreating(modelBuilder);
		}

	}
}


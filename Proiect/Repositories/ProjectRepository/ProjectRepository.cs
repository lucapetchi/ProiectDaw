using Proiect.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proiect.Repositories.ProjectRepository;
using Proiect.Data;
using Proiect.Repositories.GenericRepository;
using Task = System.Threading.Tasks.Task;
using Microsoft.AspNetCore.Mvc;

namespace Proiect.Repositories.ProjectRepository
{
	public class ProjectRepository : GenericRepository<Project>, IProjectRepository
	{
		private readonly AppDbContext db;
		public ProjectRepository(AppDbContext context) : base(context)
		{ context = db; }

		public async Task<List<Project>> GetAll() { return await GetAllAsync(); }
		public async Task<Project> GetById(Guid id) { return await FindByIdAsync(id); }
		public async Task CreateAsync(Project newProject)
		{
			var proj = new Project
			{
				Name = newProject.Name,
				Description = newProject.Description,
				organizer_id = newProject.organizer_id,
				Tasks = newProject.Tasks,
				Users = newProject.Users
			};
		}
		public async Task Update(Guid id, Project proj) {
			var curr = await FindByIdAsync(id);

			curr.Name = proj.Name;
			curr.Description = proj.Description;
			curr.Users = proj.Users;
			curr.organizer_id = proj.organizer_id;
			curr.Tasks = proj.Tasks;
			

			db.Update(curr);
			await SaveAsync();
		}
		public async Task<List<Project>> Delete(Guid id) {
			var todel = await FindByIdAsync(id);
			db.Remove(todel);
			return await GetAll();
		}
	}
}
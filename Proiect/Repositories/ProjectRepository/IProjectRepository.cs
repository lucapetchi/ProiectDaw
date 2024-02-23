using Microsoft.AspNetCore.Mvc;
using Proiect.Models;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using System;
using System.Linq;
using Proiect.Repositories.GenericRepository;

namespace Proiect.Repositories.ProjectRepository

{
	public interface IProjectRepository:IGenericRepository<Project>
	{
		public Task<List<Project>> GetAll();
		public Task<Project> GetById(Guid id);
		public Task CreateAsync(Project newProject);
		public Task Update(Guid id, Project proj);
		public Task<List<Project>> Delete(Guid id);



	}
}

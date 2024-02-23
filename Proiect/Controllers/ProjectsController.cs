using Microsoft.AspNetCore.Mvc;
using Proiect.Models;
using Proiect.Data;
using Microsoft.EntityFrameworkCore;
using Proiect.Repositories.ProjectRepository;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Proiect.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	
	public class ProjectsController : Controller
	{
		
		private readonly IProjectRepository _rep;
		public ProjectsController(IProjectRepository context)
		{
			_rep = context;
		}
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var projects = await _rep.GetAllAsync();
			return Ok(projects);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(Guid id)
		{
			var proj = await _rep.GetById(id);
			if (proj == null) return NotFound();
			return Ok(proj);
		}
		[HttpPost, Authorize(Roles ="Admin")]
		
		public async Task<IActionResult> Create([FromBody] Project project)
		{
			await _rep.CreateAsync(project);
			return Ok();
		}



		[HttpPut("{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] Project project)
		{

			var curr = await _rep.GetById(id);
			if (curr == null) return NotFound();

			await _rep.Update(id, project);
			return NoContent();
		}



		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var proj = await _rep.GetById(id);
			if (proj == null) return NotFound();

			await _rep.Delete(id);
			return NoContent();
		}
	}
}

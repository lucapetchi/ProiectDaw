using Proiect.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proiect.Repositories.GenericRepository;
using Proiect.Data;


namespace Proiect.Repositories.TaskRepository
{
	public class TaskRepository : GenericRepository<Models.Task>, ITaskRepository
	{
		private readonly AppDbContext _context;

		public TaskRepository(AppDbContext tableContext) : base(tableContext)
		{
		}

		public async Task<List<Models.Task>> GetAllAsync()
		{
			return await _context.Tasks.ToListAsync();
		}

		public async System.Threading.Tasks.Task<Models.Task> GetByIdAsync(Guid id)
		{
			return await _context.Tasks.FindAsync(id);
		}

		public async System.Threading.Tasks.Task CreateAsync(Models.Task t)
		{
			await _context.Tasks.AddAsync(t);
			await _context.SaveChangesAsync();
		}

		public async System.Threading.Tasks.Task UpdateAsync(Models.Task t)
		{
			_context.Tasks.Update(t);
			await _context.SaveChangesAsync();
		}

		public async System.Threading.Tasks.Task DeleteAsync(Guid id)
		{
			var t = await _context.Tasks.FindAsync(id);
			if (t != null)
			{
				_context.Tasks.Remove(t);
				await _context.SaveChangesAsync();
			}
		}

	}
}

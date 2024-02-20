using Proiect.Models;
namespace Proiect.Repositories.TaskRepository

{
	public interface ITaskRepository
	{
	
		System.Threading.Tasks.Task<Models.Task> GetByIdAsync(Guid id);
		System.Threading.Tasks.Task CreateAsync(Models.Task t);
		System.Threading.Tasks.Task UpdateAsync(Models.Task t);
        System.Threading.Tasks.Task DeleteAsync(Guid id);
	}
}

using Proiect.Models;
using Proiect.Repositories.GenericRepository;
using Task = System.Threading.Tasks.Task;

namespace Proiect.Repositories.UserRepository
{
	public interface IUserRepository: IGenericRepository<User>
	{
		//Task<List<User>> GetAllAsync();
		//Task<User> GetByIdAsync(Guid id);
		//Task DeleteAsync(Guid id);
		//UserRepository FindByUserName(string username);
		
	}
}

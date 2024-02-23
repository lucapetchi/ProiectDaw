using Proiect.Models.Base;
using Proiect.Data;
using Microsoft.EntityFrameworkCore;

namespace Proiect.Repositories.GenericRepository
{
	public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
	{
		protected readonly AppDbContext _dbcontext;
		protected readonly DbSet<TEntity> dentity;

		public GenericRepository(AppDbContext tableContext)
		{
			_dbcontext = tableContext;
			dentity = _dbcontext.Set<TEntity>();
		}

		public List<TEntity> GetAll()
		{
			return dentity.AsNoTracking().ToList();
		}
		public async Task<List<TEntity>> GetAllAsync()
		{
			return await dentity.AsNoTracking().ToListAsync();
		}


		public void Create(TEntity entity)
		{
			dentity.Add(entity);
		}

		public async Task CreateAsync(TEntity entity)
		{
			await dentity.AddAsync(entity);
		}

		public void CreateRange(IEnumerable<TEntity> entities)
		{
			dentity.AddRange(entities);
		}

		public async Task CreateRangeAsync(IEnumerable<TEntity> entities)
		{
			await dentity.AddRangeAsync(entities);

		}

		public void Update(TEntity entity)
		{
			dentity.Update(entity);
		}

		public void UpdateRange(IEnumerable<TEntity> entities)
		{
			dentity.UpdateRange(entities);
		}

		public void Delete(TEntity entity)
		{
			dentity.Remove(entity);
		}

		public void DeleteRange(IEnumerable<TEntity> entities)
		{
			dentity.RemoveRange(entities);
		}

		public TEntity FindById(Guid id)
		{
			return dentity.Find(id);
		}

		public async Task<TEntity> FindByIdAsync(Guid id)
		{
			return await dentity.FindAsync(id);
		}


		public bool Save()
		{
			return _dbcontext.SaveChanges() > 0;
		}
		public async Task<bool> SaveAsync()
		{
			return await _dbcontext.SaveChangesAsync() > 0;
		}


	}
}
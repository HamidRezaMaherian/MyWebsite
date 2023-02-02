using Microsoft.EntityFrameworkCore;
using MyWebsite.Application.Repositories;
using MyWebsite.Domain.Base;
using System.Linq.Expressions;

namespace MyWebsite.Infrastructure.Persistent.Repositories
{
	internal class GenericRepository<T> : IRepository<T> where T : BaseEntity
	{
		protected readonly ApplicationDbContext _db;
		protected readonly DbSet<T> _dbSet;

		public GenericRepository(ApplicationDbContext db)
		{
			ArgumentNullException.ThrowIfNull(db);
			_db = db;
			_db.Database.Migrate();
			_dbSet = _db.Set<T>();
		}
		public virtual void Add(T entity)
		{
			_dbSet.Add(entity);
		}
		public virtual async Task AddAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
		}

		public virtual void SoftDelete(object id)
		{
			var entity = Get(id);
			//if (entity == null)
			//	return false;
			entity.IsActive = false;
			entity.IsDelete = true;
		}

		public void SoftDelete(T entity)
		{
			entity.IsActive = false;
			entity.IsDelete = true;
		}
		public virtual void HardDelete(object id)
		{
			var entity = Get(id);
			//if (entity == null)
			//	return false;
			_dbSet.Remove(entity);
		}

		public void HardDelete(T entity)
		{
			_dbSet.Remove(entity);
		}

		public virtual IEnumerable<T> Get(Expression<Func<T, bool>> condition = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int skip = 0, int take = 0, bool ShowAll = false, bool asNoTracking = false)
		{
			IQueryable<T> result = _dbSet;
			if (asNoTracking)
			{
				result = result.AsNoTracking();
			}
			result = result.Where(i => i.IsDelete == false).OrderByDescending(p => p).AsQueryable();
			if (condition != null)
			{
				result = result.Where(condition);
			}
			orderBy?.Invoke(result);

			if (skip != 0 && skip > 0)
			{
				result = result.Skip(skip);
			}
			if (take != 0 && take > 0)
			{
				result = result.Take(take);
			}
			if (!ShowAll)
				result = result.Where(i => i.IsActive);
			return result.ToList();
		}

		public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> condition = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int skip = 0, int take = 0, bool ShowAll = false, bool asNoTracking = false)
		{
			IQueryable<T> result = _dbSet;
			if (asNoTracking)
			{
				result = result.AsNoTracking();
			}
			result = result.Where(i => i.IsDelete == false).OrderByDescending(p => p).AsQueryable();
			if (condition != null)
			{
				result = result.Where(condition);
			}

			orderBy?.Invoke(result);

			if (skip != 0 && skip > 0)
			{
				result = result.Skip(skip);
			}
			if (take != 0 && take > 0)
			{
				result = result.Take(take);
			}
			if (!ShowAll)
				result = result.Where(i => i.IsActive);
			//await Task.Delay(500);
			return await result.ToListAsync();
		}

		public virtual T Get(object id, bool asNoTracking = true)
		{
			var result = _dbSet.Find(id);
			if (asNoTracking)
				_db.Entry(result).State = EntityState.Detached;
			return result;
		}
		public virtual async Task<T> GetAsync(object id, bool asNoTracking = true)
		{
			var result = await _dbSet.FindAsync(id);
			if (asNoTracking)
				_db.Entry(result).State = EntityState.Detached;
			return result;
		}
		public virtual T FirstOrDefault(Expression<Func<T, bool>> condition = null, bool asNoTracking = true, string includeProps = null)
		{
			var query = _dbSet.AsQueryable();
			if (!string.IsNullOrEmpty(includeProps))
			{
				foreach (var prop in includeProps.Split(',', StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(prop);
				}
			}
			T result;

			if (condition != null)
				result = query.FirstOrDefault(condition);
			else
				result = query.FirstOrDefault();
			if (asNoTracking)
				_db.Entry(result).State = EntityState.Detached;
			return result;
		}
		public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> condition = null, bool asNoTracking = true, string includeProps = null)
		{
			var query = _dbSet.AsQueryable();
			if (!string.IsNullOrEmpty(includeProps))
			{
				foreach (var prop in includeProps.Split(',', StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(prop);
				}
			}
			T result;

			if (condition != null)
				result = await query.FirstOrDefaultAsync(condition);
			else
				result = await query.FirstOrDefaultAsync();
			if (asNoTracking)
				_db.Entry(result).State = EntityState.Detached;
			return result;
		}

		public virtual void Update(T entity)
		{
			_dbSet.Update(entity);
		}

		public void Update(object id)
		{
			var entity = Get(id);
			_dbSet.Update(entity);
		}
		public int Count(Expression<Func<T, bool>> condition = null)
		{
			return condition != null ? _dbSet.Count(condition) : _dbSet.Count();
		}
	}
}

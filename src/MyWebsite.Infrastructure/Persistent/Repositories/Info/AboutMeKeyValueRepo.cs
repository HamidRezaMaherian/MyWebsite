using Microsoft.EntityFrameworkCore;
using MyWebsite.Application.Exceptions;
using MyWebsite.Application.Repositories;
using MyWebsite.Domain.Entities.Info;
using MyWebsite.Infrastructure.Persistent;
using System.Linq.Expressions;

namespace MyWebsite.Infrastructure.Repositories.Info
{
	[RepositoryConcrete(typeof(IAboutMeKeyValueRepo))]
	internal class AboutMeKeyValueRepo : IAboutMeKeyValueRepo
	{
		private ApplicationDbContext _db;

		public AboutMeKeyValueRepo(ApplicationDbContext db)
		{
			_db = db;
		}

		public void Delete(int id)
		{
			if (id <= 0)
			{
				throw new ArgumentNullException("id cannot be less than 1");
			}
			var entity = _db.AboutMeKeyValues.AsNoTracking().FirstOrDefault(i => i.Id == id);
			if (entity is null)
				throw new DbOperationException("entity not found with given id");
			_db.AboutMeKeyValues.Remove(entity);
			_db.SaveChanges();
		}

		public AboutMeKeyValue FirstOrDefault(Expression<Func<AboutMeKeyValue, bool>> predicate)
		{
			ArgumentNullException.ThrowIfNull(predicate);
			return _db.AboutMeKeyValues.AsNoTracking().FirstOrDefault(predicate);
		}

		public AboutMeKeyValue FirstOrDefault()
		{
			return _db.AboutMeKeyValues.AsNoTracking().FirstOrDefault();
		}

		public IEnumerable<AboutMeKeyValue> GetAll(Expression<Func<AboutMeKeyValue, bool>> predicate)
		{
			ArgumentNullException.ThrowIfNull(predicate);
			return _db.AboutMeKeyValues.AsNoTracking().Where(predicate).ToList();
		}

		public IEnumerable<AboutMeKeyValue> GetAll()
		{
			return _db.AboutMeKeyValues.AsNoTracking().ToList();
		}

		public AboutMeKeyValue GetById(int id)
		{
			if (id <= 0)
				throw new ArgumentOutOfRangeException(nameof(id), "value must be greater than 0");
			return _db.AboutMeKeyValues.AsNoTracking().FirstOrDefault(i => i.Id == id);
		}

		public void Insert(AboutMeKeyValue entity)
		{
			ArgumentNullException.ThrowIfNull(entity);
			try
			{
				_db.AboutMeKeyValues.Add(entity);
				_db.SaveChanges();
			}
			catch (Exception e)
			{
				DetachEntity(entity);
				throw new DbOperationException(e.Message, e.InnerException);
			}
		}

		public void Update(AboutMeKeyValue entity)
		{
			ArgumentNullException.ThrowIfNull(entity);
			if (!_db.AboutMeKeyValues.Any(i => i.Id == entity.Id))
			{
				throw new DbOperationException("entity not found");
			}
			try
			{
				_db.AboutMeKeyValues.Update(entity);
				_db.SaveChanges();
			}
			catch (Exception e)
			{
				throw new DbOperationException(e.Message, e.InnerException);
			}
			finally
			{
				DetachEntity(entity);
			}
		}
		private void DetachEntity(AboutMeKeyValue entity)
		{
			_db.Entry(entity).State = EntityState.Detached;
		}
	}
}
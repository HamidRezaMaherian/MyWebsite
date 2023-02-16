using Microsoft.EntityFrameworkCore;
using MyWebsite.Application.Exceptions;
using MyWebsite.Application.Repositories;
using MyWebsite.Domain.Entities.Info;
using MyWebsite.Infrastructure.Persistent;
using System.Linq.Expressions;

namespace MyWebsite.Infrastructure.Repositories.Info
{
	internal class EducationRepo : IEducationRepo
	{
		private readonly ApplicationDbContext _db;

		public EducationRepo(ApplicationDbContext db)
		{
			ArgumentNullException.ThrowIfNull(db);
			_db = db;
		}

		public void Delete(int id)
		{
			if (id <= 0)
			{
				throw new ArgumentNullException("id cannot be less than 1");
			}
			var entity = _db.Educations.AsNoTracking().FirstOrDefault(i => i.Id == id);
			if (entity is null)
				throw new DbOperationException("entity not found with given id");
			_db.Educations.Remove(entity);
			_db.SaveChanges();
		}

		public Education FirstOrDefault(Expression<Func<Education, bool>> predicate)
		{
			ArgumentNullException.ThrowIfNull(predicate);
			return _db.Educations.AsNoTracking().FirstOrDefault(predicate);
		}

		public Education FirstOrDefault()
		{
			return _db.Educations.AsNoTracking().FirstOrDefault();
		}

		public IEnumerable<Education> GetAll(Expression<Func<Education, bool>> predicate)
		{
			ArgumentNullException.ThrowIfNull(predicate);
			return _db.Educations.AsNoTracking().Where(predicate).ToList();
		}

		public IEnumerable<Education> GetAll()
		{
			return _db.Educations.AsNoTracking().ToList();
		}

		public Education GetById(int id)
		{
			if (id <= 0)
				throw new ArgumentOutOfRangeException(nameof(id), "value must be greater than 0");
			return _db.Educations.AsNoTracking().FirstOrDefault(i => i.Id == id);
		}

		public void Insert(Education entity)
		{
			ArgumentNullException.ThrowIfNull(entity);
			try
			{
				_db.Educations.Add(entity);
				_db.SaveChanges();
			}
			catch (Exception e)
			{
				DetachEntity(entity);
				throw new DbOperationException(e.Message, e.InnerException);
			}
		}

		public void Update(Education entity)
		{
			ArgumentNullException.ThrowIfNull(entity);
			if (!_db.Educations.Any(i => i.Id == entity.Id))
			{
				throw new DbOperationException("entity not found");
			}
			try
			{
				_db.Educations.Update(entity);
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
		private void DetachEntity(Education entity)
		{
			_db.Entry(entity).State = EntityState.Detached;
		}
	}
}
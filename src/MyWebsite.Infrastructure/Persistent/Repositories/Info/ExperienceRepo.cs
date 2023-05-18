using Microsoft.EntityFrameworkCore;
using MyWebsite.Application.Exceptions;
using MyWebsite.Application.Repositories;
using MyWebsite.Domain.Entities.Info;
using MyWebsite.Infrastructure.Persistent;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace MyWebsite.Infrastructure.Repositories.Info
{
	[RepositoryConcrete(typeof(IExperienceRepo))]
	internal class ExperienceRepo : IExperienceRepo
	{
		private ApplicationDbContext _db;

		public ExperienceRepo(ApplicationDbContext db)
		{
			_db = db;
		}

		public void Delete(int id)
		{
			if (id <= 0)
			{
				throw new ArgumentNullException("id cannot be less than 1");
			}
			var entity = _db.Experiences.AsNoTracking().FirstOrDefault(i => i.Id == id);
			if (entity is null)
				throw new DbOperationException("entity not found with given id");
			_db.Experiences.Remove(entity);
			_db.SaveChanges();
		}

		public Experience FirstOrDefault(Expression<Func<Experience, bool>> predicate)
		{
			ArgumentNullException.ThrowIfNull(predicate);
			return _db.Experiences.AsNoTracking().FirstOrDefault(predicate);
		}

		public Experience FirstOrDefault()
		{
			return _db.Experiences.AsNoTracking().FirstOrDefault();
		}

		public IEnumerable<Experience> GetAll(Expression<Func<Experience, bool>> predicate)
		{
			ArgumentNullException.ThrowIfNull(predicate);
			return _db.Experiences.AsNoTracking().Where(predicate).ToList();
		}

		public IEnumerable<Experience> GetAll()
		{
			return _db.Experiences.AsNoTracking().ToList();
		}

		public Experience GetById(int id)
		{
			if (id <= 0)
				throw new ArgumentOutOfRangeException(nameof(id), "value must be greater than 0");
			return _db.Experiences.AsNoTracking().FirstOrDefault(i => i.Id == id);
		}

		public void Insert(Experience entity)
		{
			ArgumentNullException.ThrowIfNull(entity);
			try
			{
				_db.Experiences.Add(entity);
				_db.SaveChanges();
			}
			catch (Exception e)
			{
				DetachEntity(entity);
				throw new DbOperationException(e.Message, e.InnerException);
			}
		}

		public void Update(Experience entity)
		{
			ArgumentNullException.ThrowIfNull(entity);
			if (!_db.Experiences.Any(i => i.Id == entity.Id))
			{
				throw new DbOperationException("entity not found");
			}
			try
			{
				_db.Experiences.Update(entity);
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
		private void DetachEntity(Experience entity)
		{
			_db.Entry(entity).State = EntityState.Detached;
		}
	}
}
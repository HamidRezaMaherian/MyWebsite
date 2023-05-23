using Microsoft.EntityFrameworkCore;
using MyWebsite.Application.Exceptions;
using MyWebsite.Application.Repositories;
using MyWebsite.Domain.Entities.Info;
using MyWebsite.Infrastructure.Persistent;
using System.Linq.Expressions;

namespace MyWebsite.Infrastructure.Repositories.Info
{
	[RepositoryConcrete(typeof(ISkillRepo))]
	internal class SkillRepo : ISkillRepo
	{
		private ApplicationDbContext _db;

		public SkillRepo(ApplicationDbContext db)
		{
			_db = db;
		}

		public void Delete(int id)
		{
			if (id <= 0)
			{
				throw new ArgumentNullException("id cannot be less than 1");
			}
			var entity = _db.Skills.AsNoTracking().FirstOrDefault(i => i.Id == id);
			if (entity is null)
				throw new DbOperationException("entity not found with given id");
			_db.Skills.Remove(entity);
			_db.SaveChanges();
		}

		public Skill FirstOrDefault(Expression<Func<Skill, bool>> predicate)
		{
			ArgumentNullException.ThrowIfNull(predicate);
			return _db.Skills.AsNoTracking().FirstOrDefault(predicate);
		}

		public Skill FirstOrDefault()
		{
			return _db.Skills.AsNoTracking().FirstOrDefault();
		}

		public IEnumerable<Skill> GetAll(Expression<Func<Skill, bool>> predicate)
		{
			ArgumentNullException.ThrowIfNull(predicate);
			return _db.Skills.AsNoTracking().Where(predicate).ToList();
		}

		public IEnumerable<Skill> GetAll()
		{
			return _db.Skills.AsNoTracking().ToList();
		}

		public Skill GetById(int id)
		{
			if (id <= 0)
				throw new ArgumentOutOfRangeException(nameof(id), "value must be greater than 0");
			return _db.Skills.AsNoTracking().FirstOrDefault(i => i.Id == id);
		}

		public void Insert(Skill entity)
		{
			ArgumentNullException.ThrowIfNull(entity);
			try
			{
				_db.Skills.Add(entity);
				_db.SaveChanges();
			}
			catch (Exception e)
			{
				DetachEntity(entity);
				throw new DbOperationException(e.Message, e.InnerException);
			}
		}

		public void Update(Skill entity)
		{
			ArgumentNullException.ThrowIfNull(entity);
			if (!_db.Skills.Any(i => i.Id == entity.Id))
			{
				throw new DbOperationException("entity not found");
			}
			try
			{
				_db.Skills.Update(entity);
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
		private void DetachEntity(Skill entity)
		{
			_db.Entry(entity).State = EntityState.Detached;
		}

	}
}
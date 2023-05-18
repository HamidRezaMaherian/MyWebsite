using Microsoft.EntityFrameworkCore;
using MyWebsite.Application.Exceptions;
using MyWebsite.Application.Repositories;
using MyWebsite.Domain.Entities.Info;
using MyWebsite.Infrastructure.Persistent;
using System.Linq.Expressions;

namespace MyWebsite.Infrastructure.Repositories.Info
{
	[RepositoryConcrete(typeof(IProjectRepo))]
	internal class ProjectRepo : IProjectRepo
	{
		private ApplicationDbContext _db;
		public ProjectRepo(ApplicationDbContext db)
		{
			_db = db;
		}

		public void Delete(int id)
		{
			if (id <= 0)
			{
				throw new ArgumentNullException("id cannot be less than 1");
			}
			var entity = _db.Projects.AsNoTracking().FirstOrDefault(i => i.Id == id);
			if (entity is null)
				throw new DbOperationException("entity not found with given id");
			_db.Projects.Remove(entity);
			_db.SaveChanges();
		}

		public Project FirstOrDefault(Expression<Func<Project, bool>> predicate)
		{
			ArgumentNullException.ThrowIfNull(predicate);
			return _db.Projects.AsNoTracking().FirstOrDefault(predicate);
		}

		public Project FirstOrDefault()
		{
			return _db.Projects.AsNoTracking().FirstOrDefault();
		}

		public IEnumerable<Project> GetAll(Expression<Func<Project, bool>> predicate)
		{
			ArgumentNullException.ThrowIfNull(predicate);
			return _db.Projects.AsNoTracking().Where(predicate).ToList();
		}

		public IEnumerable<Project> GetAll()
		{
			return _db.Projects.AsNoTracking().ToList();
		}

		public Project GetById(int id)
		{
			if (id <= 0)
				throw new ArgumentOutOfRangeException(nameof(id), "value must be greater than 0");
			return _db.Projects.AsNoTracking().FirstOrDefault(i => i.Id == id);
		}

		public void Insert(Project entity)
		{
			ArgumentNullException.ThrowIfNull(entity);
			try
			{
				_db.Projects.Add(entity);
				_db.SaveChanges();
			}
			catch (Exception e)
			{
				DetachEntity(entity);
				throw new DbOperationException(e.Message, e.InnerException);
			}
		}

		public void Update(Project entity)
		{
			ArgumentNullException.ThrowIfNull(entity);
			if (!_db.Projects.Any(i => i.Id == entity.Id))
			{
				throw new DbOperationException("entity not found");
			}
			try
			{
				_db.Projects.Update(entity);
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
		private void DetachEntity(Project entity)
		{
			_db.Entry(entity).State = EntityState.Detached;
		}
	}
}
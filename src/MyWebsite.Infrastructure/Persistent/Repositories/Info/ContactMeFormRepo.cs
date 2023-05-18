using Microsoft.EntityFrameworkCore;
using MyWebsite.Application.Exceptions;
using MyWebsite.Application.Repositories;
using MyWebsite.Domain.Entities.Info;
using MyWebsite.Infrastructure.Persistent;
using System.Linq.Expressions;

namespace MyWebsite.Infrastructure.Tests.Repositories.Info
{
	[RepositoryConcrete(typeof(IContactMeFormRepo))]
	internal class ContactMeFormRepo : IContactMeFormRepo
	{
		private ApplicationDbContext _db;

		public ContactMeFormRepo(ApplicationDbContext db)
		{
			_db = db;
		}

		public void Delete(int id)
		{
			if (id <= 0)
			{
				throw new ArgumentNullException("id cannot be less than 1");
			}
			var entity = _db.ContactMeForm.AsNoTracking().FirstOrDefault(i => i.Id == id);
			if (entity is null)
				throw new DbOperationException("entity not found with given id");
			_db.ContactMeForm.Remove(entity);
			_db.SaveChanges();
		}

		public ContactMeForm FirstOrDefault(Expression<Func<ContactMeForm, bool>> predicate)
		{
			ArgumentNullException.ThrowIfNull(predicate);
			return _db.ContactMeForm.AsNoTracking().FirstOrDefault(predicate);
		}

		public ContactMeForm FirstOrDefault()
		{
			return _db.ContactMeForm.AsNoTracking().FirstOrDefault();
		}

		public IEnumerable<ContactMeForm> GetAll(Expression<Func<ContactMeForm, bool>> predicate)
		{
			ArgumentNullException.ThrowIfNull(predicate);
			return _db.ContactMeForm.AsNoTracking().Where(predicate).ToList();
		}

		public IEnumerable<ContactMeForm> GetAll()
		{
			return _db.ContactMeForm.AsNoTracking().ToList();
		}

		public ContactMeForm GetById(int id)
		{
			if (id <= 0)
				throw new ArgumentOutOfRangeException(nameof(id), "value must be greater than 0");
			return _db.ContactMeForm.AsNoTracking().FirstOrDefault(i => i.Id == id);
		}

		public void Insert(ContactMeForm entity)
		{
			ArgumentNullException.ThrowIfNull(entity);
			try
			{
				_db.ContactMeForm.Add(entity);
				_db.SaveChanges();
			}
			catch (Exception e)
			{
				DetachEntity(entity);
				throw new DbOperationException(e.Message, e.InnerException);
			}
		}

		public void Update(ContactMeForm entity)
		{
			ArgumentNullException.ThrowIfNull(entity);
			if (!_db.ContactMeForm.Any(i => i.Id == entity.Id))
			{
				throw new DbOperationException("entity not found");
			}
			try
			{
				_db.ContactMeForm.Update(entity);
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
		private void DetachEntity(ContactMeForm entity)
		{
			_db.Entry(entity).State = EntityState.Detached;
		}
	}
}
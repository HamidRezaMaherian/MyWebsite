using Microsoft.EntityFrameworkCore;
using MyWebsite.Domain.Entities.Info;
using MyWebsite.Infrastructure.Persistent;
using System.Linq.Expressions;

namespace MyWebsite.Infrastructure.Repositories.Info
{
	internal class ContactMeRepo
	{
		private readonly ApplicationDbContext _db;

		public ContactMeRepo(ApplicationDbContext db)
		{
			ArgumentNullException.ThrowIfNull(db, nameof(db));
			_db = db;
		}
		public async Task<ContactMe> FirstOrDefaultAsync(Expression<Func<ContactMe, bool>> condition = null)
		{
			return
				condition is null ?
				await _db.ContactMe.AsNoTracking().FirstOrDefaultAsync() :
				await _db.ContactMe.AsNoTracking().FirstOrDefaultAsync(condition);
		}

		public async Task UpdateAsync(ContactMe entity)
		{
			ArgumentNullException.ThrowIfNull(entity);
			try
			{
				_db.Update(entity);
				await _db.SaveChangesAsync();
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
				_db.Entry(entity).State = EntityState.Detached;
			}
		}
	}
}
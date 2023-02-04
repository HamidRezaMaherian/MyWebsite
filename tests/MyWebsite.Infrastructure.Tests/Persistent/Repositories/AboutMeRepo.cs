using Microsoft.EntityFrameworkCore;
using MyWebsite.Application.Repositories;
using MyWebsite.Domain.Entities.Info;
using MyWebsite.Infrastructure.Persistent;
using System.Linq.Expressions;

namespace MyWebsite.Infrastructure.Tests.Repositories.Info
{
	internal class AboutMeRepo : IAboutMeRepo
	{
		private ApplicationDbContext _db;
		public AboutMeRepo(ApplicationDbContext db)
		{
			ArgumentNullException.ThrowIfNull(db, nameof(db));
			_db = db;
		}

		public AboutMe FirstOrDefault(Expression<Func<AboutMe, bool>> condition = null)
		{
			return FirstOrDefaultAsync(condition).Result;
		}

		public async Task<AboutMe> FirstOrDefaultAsync(Expression<Func<AboutMe, bool>> condition = null)
		{
			return
				condition is null ?
				await _db.AboutMe.AsNoTracking().FirstOrDefaultAsync() :
				await _db.AboutMe.AsNoTracking().FirstOrDefaultAsync(condition);
		}

		public void Update(AboutMe entity)
		{
			UpdateAsync(entity).GetAwaiter().GetResult();
		}

		public async Task UpdateAsync(AboutMe entity)
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
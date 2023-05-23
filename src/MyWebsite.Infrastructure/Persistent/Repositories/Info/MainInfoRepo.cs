using Microsoft.EntityFrameworkCore;
using MyWebsite.Application.Exceptions;
using MyWebsite.Application.Repositories;
using MyWebsite.Domain.Entities.Info;
using MyWebsite.Infrastructure.Persistent;
using System.Linq.Expressions;

namespace MyWebsite.Infrastructure.Repositories.Info
{
	[RepositoryConcrete(typeof(IMainInfoRepo))]
	internal class MainInfoRepo : IMainInfoRepo
	{
		private ApplicationDbContext _db;

		public MainInfoRepo(ApplicationDbContext db)
		{
			ArgumentNullException.ThrowIfNull(db);
			_db = db;
		}
		public MainInfo FirstOrDefault(Expression<Func<MainInfo, bool>> condition = null)
		{
			return FirstOrDefaultAsync(condition).Result;
		}

		public async Task<MainInfo> FirstOrDefaultAsync(Expression<Func<MainInfo, bool>> condition = null)
		{
			return
				condition is null ?
				await _db.MainInfos.AsNoTracking().FirstOrDefaultAsync() :
				await _db.MainInfos.AsNoTracking().FirstOrDefaultAsync(condition);
		}

		public void Update(MainInfo entity)
		{
			ArgumentNullException.ThrowIfNull(entity);
			try
			{
				UpdateAsync(entity).Wait();
			}
			catch (Exception e)
			{
				throw new DbOperationException(e.Message, e.InnerException);
			}
		}

		public async Task UpdateAsync(MainInfo entity)
		{
			ArgumentNullException.ThrowIfNull(entity);
			try
			{
				_db.Update(entity);
				await _db.SaveChangesAsync();
			}
			catch (Exception e)
			{
				throw new DbOperationException(e.Message, e.InnerException);
			}
			finally
			{
				_db.Entry(entity).State = EntityState.Detached;
			}
		}
	}
}
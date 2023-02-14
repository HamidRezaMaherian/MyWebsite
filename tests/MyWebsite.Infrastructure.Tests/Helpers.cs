using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MyWebsite.Domain.Entities.Info;
using MyWebsite.Infrastructure.Persistent;
using System.Diagnostics.CodeAnalysis;

namespace MyWebsite.Infrastructure.Tests;
public static class Helpers
{
	internal class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
	{
		public ApplicationDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
			optionsBuilder.UseSqlServer(Statics.dbConnectionString);

			return new ApplicationDbContext(optionsBuilder.Options);
		}
	}
	public class EducationValueComparer : IEqualityComparer<Education>
	{
		public bool Equals(Education x, Education y)
		{
			return x.Id == y.Id
				 && x.Title == y.Title
				 && x.SubTitle == y.SubTitle
				 && x.LangId == y.LangId
				 && x.Link == y.Link
				 && x.Role == y.Role
				 && x.TimeSpan == y.TimeSpan;
		}

		public int GetHashCode([DisallowNull] Education obj)
		{
			return obj.GetHashCode();
		}
	}
}
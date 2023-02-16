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
	public class ExperienceValueComparer : IEqualityComparer<Experience>
	{
		public bool Equals(Experience x, Experience y)
		{
			return x.Id == y.Id
				 && x.Title == y.Title
				 && x.SubTitle == y.SubTitle
				 && x.LangId == y.LangId
				 && x.Link == y.Link
				 && x.Role == y.Role
				 && x.TimeSpan == y.TimeSpan;
		}

		public int GetHashCode([DisallowNull] Experience obj)
		{
			return obj.GetHashCode();
		}
	}
	internal class ProjectValueComparer : IEqualityComparer<Project>
	{
		public bool Equals(Project x, Project y)
		{
			return x.Id == y.Id
				&& x.Name == y.Name
				&& x.Type == y.Type
				&& x.LangId == y.LangId
				&& x.DateTime == y.DateTime
				&& x.ImagePath == y.ImagePath
				&& x.Techs == y.Techs
				&& x.Role == y.Role
				&& x.Link == y.Link;
		}

		public int GetHashCode([DisallowNull] Project obj)
		{
			return obj.GetHashCode();
		}
	}

}
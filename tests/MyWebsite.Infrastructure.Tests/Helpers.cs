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
	internal class SkillValueComparer : IEqualityComparer<Skill>
	{
		public bool Equals(Skill x, Skill y)
		{
			return x.Id == y.Id
				&& x.Name == y.Name
				&& x.Value == y.Value
				&& x.LangId == y.LangId
				&& x.IsActive == y.IsActive;
		}

		public int GetHashCode([DisallowNull] Skill obj)
		{
			return obj.GetHashCode();
		}
	}
	internal class ContactMeFormValueComparer : IEqualityComparer<ContactMeForm>
	{
		public bool Equals(ContactMeForm x, ContactMeForm y)
		{
			return x.Id == y.Id
				&& x.Name == y.Name
				&& x.Email == y.Email
				&& x.Answer == y.Answer
				&& x.Message == y.Message
				&& x.AnswerDateTime == y.AnswerDateTime
				&& x.IsAnswered == y.IsAnswered
				&& x.QuestionDateTime == y.QuestionDateTime
				&& x.Subject == y.Subject
				&& x.UserId == y.UserId
				&& x.IsActive == y.IsActive;
		}

		public int GetHashCode([DisallowNull] ContactMeForm obj)
		{
			return obj.GetHashCode();
		}
	}
	internal class MainInfoValueComparer : IEqualityComparer<MainInfo>
	{
		public bool Equals(MainInfo x, MainInfo y)
		{
			return x.Id == y.Id
				&& x.Description == y.Description
				&& x.DarkImagePath == y.DarkImagePath
				&& x.LightImagePath == y.LightImagePath
				&& x.LangId == y.LangId
				&& x.IsActive == y.IsActive;
		}

		public int GetHashCode([DisallowNull] MainInfo obj)
		{
			return obj.GetHashCode();
		}
	}
	internal class AboutMeKeyValueComparer : IEqualityComparer<AboutMeKeyValue>
	{
		public bool Equals(AboutMeKeyValue x, AboutMeKeyValue y)
		{
			return x.Id == y.Id
				&& x.Key == y.Key
				&& x.Value == y.Value
				&& x.LangId == y.LangId
				&& x.IsActive == y.IsActive;
		}

		public int GetHashCode([DisallowNull] AboutMeKeyValue obj)
		{
			return obj.GetHashCode();
		}
	}

}
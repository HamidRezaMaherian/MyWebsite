namespace MyWebsite.Infrastructure.Tests.Repositories.Info;
using Bogus;
using static MyWebsite.Infrastructure.Tests.Helpers;
using MyWebsite.Application.Repositories;
using MyWebsite.Domain.Entities.Info;
using MyWebsite.Infrastructure.Repositories.Info;

[TestFixture]
public class ExperienceRepoTests : BaseRepoTests<Experience, IExperienceRepo>
{
	protected override IExperienceRepo CreateRepo()
	{
		return new ExperienceRepo(_db);
	}

	protected override Faker<Experience> CreateDataFaker()
	{
		return new Faker<Experience>()
							.RuleSet("RequiredProps", (rs) =>
							{
								rs.RuleFor(i => i.Title, f => f.Name.JobTitle());
								rs.RuleFor(i => i.SubTitle, f => f.Name.JobDescriptor());
								rs.RuleFor(i => i.LangId, f => f.Random.Int(1, 2));
								rs.RuleFor(i => i.IsActive, f => f.Random.Bool());
							})
							.RuleSet("OptionalProps", (rs) =>
							{
								rs.RuleFor(i => i.Link, f => f.Internet.Url());
								rs.RuleFor(i => i.Role, f => f.Name.JobType());
								rs.RuleFor(i => i.TimeSpan, f => f.Date.Timespan().ToString());
							});
	}

	protected override IEqualityComparer<Experience> CreateComparer()
	{
		return new ExperienceValueComparer();
	}
}
using Bogus;
using MyWebsite.Application.Repositories;
using MyWebsite.Domain.Entities.Info;
using MyWebsite.Infrastructure.Repositories.Info;
using static MyWebsite.Infrastructure.Tests.Helpers;

namespace MyWebsite.Infrastructure.Tests.Repositories.Info;

[TestFixture]
public class EducationRepoTests : BaseLanguageRepoTests<Education, IEducationRepo>
{
	protected override IEducationRepo CreateRepo()
	{
		return new EducationRepo(_db);
	}

	protected override Faker<Education> CreateDataFaker()
	{
		return new Faker<Education>()
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

	protected override IEqualityComparer<Education> CreateComparer()
	{
		return new EducationValueComparer();
	}
}
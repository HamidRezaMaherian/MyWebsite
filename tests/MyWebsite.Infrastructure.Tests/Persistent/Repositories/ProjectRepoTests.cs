namespace MyWebsite.Infrastructure.Tests.Repositories.Info;
using Bogus;
using MyWebsite.Application.Repositories;
using MyWebsite.Domain.Entities.Info;
using MyWebsite.Infrastructure.Repositories.Info;
using static MyWebsite.Infrastructure.Tests.Helpers;

[TestFixture]
public class ProjectRepoTests : BaseLanguageRepoTests<Project, IProjectRepo>
{
	protected override IProjectRepo CreateRepo()
	{
		return new ProjectRepo(_db);
	}

	protected override Faker<Project> CreateDataFaker()
	{
		return new Faker<Project>()
							.RuleSet("RequiredProps", (rs) =>
							{
								rs.RuleFor(i => i.Name, f => f.Name.JobTitle());
								rs.RuleFor(i => i.ImagePath, f => f.Internet.UrlRootedPath());
								rs.RuleFor(i => i.LangId, f => f.Random.Int(1, 2));
								rs.RuleFor(i => i.IsActive, f => f.Random.Bool());
								rs.RuleFor(i => i.Link, f => f.Internet.Url());
								rs.RuleFor(i => i.Role, f => f.Name.JobType());
								rs.RuleFor(i => i.DateTime, f => f.Date.Timespan().ToString());
								rs.RuleFor(i => i.Techs, f => f.Name.JobDescriptor().ToString());
								rs.RuleFor(i => i.Type, f => f.Name.JobType().ToString());
							})
							.RuleSet("OptionalProps", (rs) =>
							{
							});
	}

	protected override IEqualityComparer<Project> CreateComparer()
	{
		return new ProjectValueComparer();
	}
}
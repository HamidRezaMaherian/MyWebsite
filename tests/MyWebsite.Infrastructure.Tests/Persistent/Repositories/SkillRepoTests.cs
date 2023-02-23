namespace MyWebsite.Infrastructure.Tests.Repositories.Info;
using Bogus;
using MyWebsite.Application.Repositories;
using MyWebsite.Domain.Entities.Info;
using MyWebsite.Infrastructure.Repositories.Info;
using static MyWebsite.Infrastructure.Tests.Helpers;

[TestFixture]
public class SkillRepoTests : BaseLanguageRepoTests<Skill, ISkillRepo>
{
	protected override ISkillRepo CreateRepo()
	{
		return new SkillRepo(_db);
	}

	protected override Faker<Skill> CreateDataFaker()
	{
		return new Faker<Skill>()
							.RuleSet("RequiredProps", (rs) =>
							{
								rs.RuleFor(i => i.Name, f => f.Name.JobTitle());
								rs.RuleFor(i => i.Value, f => f.Random.Int(1, 100));
								rs.RuleFor(i => i.LangId, f => f.Random.Int(1, 2));
								rs.RuleFor(i => i.IsActive, f => f.Random.Bool());
							})
							.RuleSet("OptionalProps", (rs) =>
							{
							});
	}

	protected override IEqualityComparer<Skill> CreateComparer()
	{
		return new SkillValueComparer();
	}
}
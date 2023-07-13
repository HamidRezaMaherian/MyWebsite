using Bogus;
using MyWebsite.Domain.Entities.Info;
using MyWebsite.Presentation.UnitTests.Utils;

public class FakeSkillDataGenerator : Faker<Skill>
{
	public FakeSkillDataGenerator()
	{
		RuleSet(Helpers.Statics.REQUIRED_PROPS, (rs) =>
		 {
			 rs.RuleFor(i => i.Name, f => f.Name.JobTitle());
			 rs.RuleFor(i => i.Value, f => f.Random.Int(1, 100));
			 rs.RuleFor(i => i.LangId, f => f.Random.Int(1, 2));
			 rs.RuleFor(i => i.IsActive, f => f.Random.Bool());
		 });
	}
}

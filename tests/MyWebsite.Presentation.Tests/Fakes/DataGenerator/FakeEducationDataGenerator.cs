using Bogus;
using MyWebsite.Domain.Entities.Info;
using MyWebsite.Presentation.Tests.Utils;

public class FakeEducationDataGenerator : Faker<Education>
{

	public FakeEducationDataGenerator()
	{
		RuleSet(Helpers.Statics.REQUIRED_PROPS, (rs) =>
		 {
			 rs.RuleFor(i => i.Title, f => f.Name.JobTitle());
			 rs.RuleFor(i => i.SubTitle, f => f.Name.JobDescriptor());
			 rs.RuleFor(i => i.LangId, f => f.Random.Int(1, 2));
			 rs.RuleFor(i => i.IsActive, f => f.Random.Bool());
		 });
		RuleSet(Helpers.Statics.OPTIONAL_PROPS, (rs) =>
			{
				rs.RuleFor(i => i.Link, f => f.Internet.Url());
				rs.RuleFor(i => i.Role, f => f.Name.JobType());
				rs.RuleFor(i => i.TimeSpan, f => f.Date.Timespan().ToString());
			});
	}
}

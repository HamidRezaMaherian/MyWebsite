namespace MyWebsite.Infrastructure.Tests.Repositories.Info;
using Bogus;
using MyWebsite.Application.Repositories;
using MyWebsite.Domain.Entities.Info;
using MyWebsite.Infrastructure.Repositories.Info;
using static MyWebsite.Infrastructure.Tests.Helpers;

[TestFixture]
public class ContactMeFormRepoTests : BaseRepoTests<ContactMeForm, IContactMeFormRepo>
{
	protected override IContactMeFormRepo CreateRepo()
	{
		return new ContactMeFormRepo(_db);
	}

	protected override Faker<ContactMeForm> CreateDataFaker()
	{
		return new Faker<ContactMeForm>()
							.RuleSet("RequiredProps", (rs) =>
							{
								rs.RuleFor(i => i.Name, f => f.Name.JobTitle());
								rs.RuleFor(i => i.Email, f => f.Person.Email.ToString());
								rs.RuleFor(i => i.Subject, f => f.Name.JobArea());
								rs.RuleFor(i => i.Message, f => f.Lorem.Text());
								rs.RuleFor(i => i.IsActive, f => f.Random.Bool());
							})
							.RuleSet("OptionalProps", (rs) =>
							{
								rs.RuleFor(i => i.UserId, f => "fc6daba2-b71e-4da6-833f-090a3d3c5824");
								rs.RuleFor(i => i.Answer, f => f.Lorem.Text());
								rs.RuleFor(i => i.QuestionDateTime, f => f.Date.Recent());
								rs.RuleFor(i => i.AnswerDateTime, f => f.Date.Future());
								rs.RuleFor(i => i.IsAnswered, f => f.Random.Bool());
							});
	}

	protected override IEqualityComparer<ContactMeForm> CreateComparer()
	{
		return new ContactMeFormValueComparer();
	}
}
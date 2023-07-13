using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MyWebsite.Application.Repositories;
using MyWebsite.Presentation.Controllers;
using MyWebsite.Presentation.Model;
using MyWebsite.Presentation.Resources;
using MyWebsite.Presentation.UnitTests.Utils;
using System.Collections.Immutable;
using static MyWebsite.Presentation.UnitTests.Utils.Helpers;

namespace MyWebsite.Presentation.Tests.Unit
{
	[TestFixture]
	public class HomeControllerTests
	{
		private TestingWebAppFactory<Program> _application;
		private HomeController _controller;

		[SetUp]
		public void SetUp()
		{
			_application = new TestingWebAppFactory<Program>((serviceCollection) =>
			{
				serviceCollection.RegisterFakeRepositories();
				serviceCollection.AddScoped<HomeController>();
			});
			_controller = _application.Services.CreateScope().ServiceProvider.GetService<HomeController>();
		}

		[Test]
		public void CreateController()
		{
			Assert.That(_controller, Is.Not.Null);
		}
		[Test]
		public void Index_ReturnViewWithAppropriateModel()
		{
			var validModel = new HomeVM()
			{
				TempInfo = InjectService<IMainInfoRepo>().FirstOrDefault(),
				Projects = InjectService<IProjectRepo>().GetAll().ToImmutableList(),
				AboutMeKeyValues = InjectService<IAboutMeKeyValueRepo>().GetAll().ToImmutableList(),
				Skills = InjectService<ISkillRepo>().GetAll().ToImmutableList(),
				Experiences = InjectService<IExperienceRepo>().GetAll().ToImmutableList(),
				Educations = InjectService<IEducationRepo>().GetAll().ToImmutableList(),
				ContactMe = InjectService<IContactMeRepo>().FirstOrDefault(),
				AboutMe = InjectService<IAboutMeRepo>().FirstOrDefault()
			};

			var result = _controller!.Index();


			Assert.That(result, Is.InstanceOf<ViewResult>());
			var viewResult = result as ViewResult;
			Assert.That(viewResult!.ViewName, Is.EqualTo("Index").Or.EqualTo(null));
			var model = viewResult!.Model as HomeVM;
			Assert.That(viewResult!.Model, Is.InstanceOf<HomeVM>());
			Assert.That(new HomeVMValueComparer().Equals(model, validModel));
		}
		[Test]
		public void SwitchLang_PassNull_ReturnBadRequest()
		{
			var result = _controller.SwitchLanguage(null);
			Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
			Assert.That((result as BadRequestObjectResult).Value, Is.EqualTo(string.Format(ErrorResource.RequiredParameter, "Language")));
		}
		[Test]
		public void SwitchLang_PassInvalidLocale_ReturnBadRequest()
		{
			var result = _controller.SwitchLanguage(Guid.NewGuid().ToString());
			Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
			Assert.That((result as BadRequestObjectResult).Value, Is.EqualTo(string.Format(ErrorResource.InvalidLocale)));
		}
		[Test]
		public void SwitchLang_PassValidLocale_ReturnRedirectResult()
		{
			var result = _controller.SwitchLanguage("fa-IR");
			Assert.That(result, Is.InstanceOf<RedirectResult>());
			Assert.That((result as RedirectResult)!.Url, Is.EqualTo("/"));
		}
		private T InjectService<T>()
		{
			return _application.Services.CreateScope().ServiceProvider.GetService<T>();
		}
	}
}

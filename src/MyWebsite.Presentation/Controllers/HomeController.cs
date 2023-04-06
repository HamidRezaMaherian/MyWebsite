using Microsoft.AspNetCore.Mvc;
using MyWebsite.Application.Repositories;
using MyWebsite.Presentation.Model;
using System.Collections.Immutable;

namespace MyWebsite.Presentation.Controllers
{
	public class HomeController : Controller
	{
		private readonly IMainInfoRepo _mainRepo;
		private readonly IProjectRepo _projectRepo;
		private readonly IAboutMeKeyValueRepo _aboutMeKeyValueRepo;
		private readonly ISkillRepo _skillRepo;
		private readonly IExperienceRepo _experienceRepo;
		private readonly IEducationRepo _educationRepo;
		private readonly IContactMeRepo _contactMeRepo;
		private readonly IAboutMeRepo _aboutMeRepo;

		public HomeController(
			IMainInfoRepo mainRepo,
			IProjectRepo projectRepo,
			IAboutMeKeyValueRepo aboutMeKeyValueRepo,
			ISkillRepo skillRepo,
			IExperienceRepo experienceRepo,
			IEducationRepo educationRepo,
			IContactMeRepo contactMeRepo,
			IAboutMeRepo aboutMeRepo)
		{
			_mainRepo = mainRepo;
			_projectRepo = projectRepo;
			_aboutMeKeyValueRepo = aboutMeKeyValueRepo;
			_skillRepo = skillRepo;
			_experienceRepo = experienceRepo;
			_educationRepo = educationRepo;
			_contactMeRepo = contactMeRepo;
			_aboutMeRepo = aboutMeRepo;
		}
		public IActionResult Index()
		{
			var model = new HomeVM()
			{
				TempInfo = _mainRepo.FirstOrDefault(),
				Projects = _projectRepo.GetAll().ToImmutableList(),
				AboutMeKeyValues = _aboutMeKeyValueRepo.GetAll().ToImmutableList(),
				Skills = _skillRepo.GetAll().ToImmutableList(),
				Experiences = _experienceRepo.GetAll().ToImmutableList(),
				Educations = _educationRepo.GetAll().ToImmutableList(),
				ContactMe = _contactMeRepo.FirstOrDefault(),
				AboutMe = _aboutMeRepo.FirstOrDefault()
			};
			return View(model);
		}
	}
}
using Microsoft.AspNetCore.Identity;
using MyWebsite.Domain.Entities;
using MyWebsite.Domain.Entities.Info;
using MyWebsite.Domain.Entities.User;
using MyWebsite.Shared.Utilities;

namespace MyWebsite.Infrastructure.Persistent
{
	internal static class SeedDataCreator
	{
		public static IEnumerable<Language> CreateLanguage()
		{
			return new Language[]
			{
				new Language() { Id = 1, Culture = "fa-IR", IsActive = true },
				new Language() { Id = 2, Culture = "en-US", IsActive = true }
			};
		}
		public static IdentityRole CreateRole()
		{
			return new IdentityRole() { Id = "019d60d6-3dbe-413d-8e76-849dc232df42", Name = Statics.Roles.SuperAdmin, NormalizedName = Statics.Roles.SuperAdmin.ToUpper() };
		}
		public static ApplicationUser CreateDefaultUser()
		{
			return new ApplicationUser() { Id = "fc6daba2-b71e-4da6-833f-090a3d3c5824", SignUpDateTime = DateTime.Parse("3/29/2023"), PhoneNumber = "09127337442", ImagePath = "avatar.png", Email = "admin@admin.com", NormalizedEmail = "ADMIN@ADMIN.COM", UserName = "admin", NormalizedUserName = "ADMIN", IsActive = true, PasswordHash = "AQAAAAEAACcQAAAAEJmA1NW1DTVMbtuO0Pp58yG9lgQtITsZK9OiMk0fsU6Nwy5RFK3NygEAmvjswiTW+Q==" };
		}
		public static IdentityUserRole<string> CreateUserRole()
		{
			return new IdentityUserRole<string>() { RoleId = "019d60d6-3dbe-413d-8e76-849dc232df42", UserId = "fc6daba2-b71e-4da6-833f-090a3d3c5824" };
		}
		public static IEnumerable<AboutMe> CreateAboutMe()
		{
			return new AboutMe[]{
					new AboutMe() { Id = 1, LangId = 1, FilePath = "no file", IsActive = true },
					new AboutMe() { Id = 2, LangId = 2, FilePath = "no file", IsActive = true }
			};
		}
		public static IEnumerable<ContactMe> CreateContactMe()
		{
			return new ContactMe[]{
					new ContactMe() { Id = 1, LangId = 1, PhoneNumber = "09304422204", Email = "test@test", IsActive = true },
					new ContactMe() { Id = 2, LangId = 2, PhoneNumber = "09304422204", Email = "test@test", IsActive = true }
			};
		}
		public static IEnumerable<MainInfo> CreateMainInfo()
		{
			return new MainInfo[]{
				new MainInfo() { Id = 1, LangId = 1, Description = "no desc", DarkImagePath = "no image", LightImagePath = "no image", IsActive = true },
				new MainInfo() { Id = 2, LangId = 2, Description = "no desc", DarkImagePath = "no image", LightImagePath = "no image", IsActive = true }
				};
		}
	}
}
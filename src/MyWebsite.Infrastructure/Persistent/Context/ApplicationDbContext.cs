using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyWebsite.Domain.Entities;
using MyWebsite.Domain.Entities.Info;
using MyWebsite.Domain.Entities.User;
using MyWebsite.Shared.Utilities;

namespace MyWebsite.Infrastructure.Persistent
{
	internal class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext()
		{

		}
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			 : base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.HasDefaultSchema("dbo");
			builder.Entity<IdentityRole>().Property(e => e.Id).ValueGeneratedOnAdd();
			builder.Entity<ApplicationUser>().Property(e => e.Id).ValueGeneratedOnAdd();
			SeedData(builder);

			base.OnModelCreating(builder);
		}
		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		public DbSet<Language> Languages { get; set; }

		#region Info
		public DbSet<ContactMe> ContactMe { get; set; }
		public DbSet<ContactMeForm> ContactMeForm { get; set; }

		public DbSet<AboutMeKeyValue> AboutMeKeyValues { get; set; }
		public DbSet<AboutMe> AboutMe { get; set; }
		public DbSet<Skill> Skills { get; set; }
		public DbSet<Project> Projects { get; set; }

		public DbSet<History> Histories { get; set; }
		public DbSet<Experience> Experiences { get; set; }
		public DbSet<Education> Educations { get; set; }

		public DbSet<MainInfo> MainInfos { get; set; }
		public DbSet<FirstTempInfo> FirstTempInfos { get; set; }
		public DbSet<SecondTempInfo> SecondTempInfos { get; set; }

		#endregion

		public DbSet<SiteVisit> SiteVisits { get; set; }
		public DbSet<ActivityLog> ActivityLogs { get; set; }

		public void SeedData(ModelBuilder builder)
		{
			builder.Entity<Language>().HasData(
				new Language() { Id = 1, Culture = "fa-IR", IsActive = true },
				new Language() { Id = 2, Culture = "en-US", IsActive = true }
				);
			builder.Entity<IdentityRole>().HasData(
				 new IdentityRole() { Id = "019d60d6-3dbe-413d-8e76-849dc232df42", Name = Statics.Roles.SuperAdmin, NormalizedName = Statics.Roles.SuperAdmin.ToUpper() });
			builder.Entity<ApplicationUser>().HasData(
				 new ApplicationUser() { Id = "fc6daba2-b71e-4da6-833f-090a3d3c5824", SignUpDateTime = DateTime.Now, PhoneNumber = "09127337442", ImagePath = "avatar.png", Email = "admin@admin.com", NormalizedEmail = "ADMIN@ADMIN.COM", UserName = "admin", NormalizedUserName = "ADMIN", IsActive = true, PasswordHash = "AQAAAAEAACcQAAAAEJmA1NW1DTVMbtuO0Pp58yG9lgQtITsZK9OiMk0fsU6Nwy5RFK3NygEAmvjswiTW+Q==" }
				 );
			builder.Entity<IdentityUserRole<string>>().HasData(
				 new IdentityUserRole<string>() { RoleId = "019d60d6-3dbe-413d-8e76-849dc232df42", UserId = "fc6daba2-b71e-4da6-833f-090a3d3c5824" });

			foreach (var item in Enumerable.Range(1, 2))
			{
				builder.Entity<AboutMe>().HasData(
					new AboutMe() { Id = item, LangId = item, FilePath = "no file", IsActive = true }
					);
				builder.Entity<ContactMe>().HasData(
					 new ContactMe() { Id = item, LangId = item, PhoneNumber = "09304422204", Email = "test@test", IsActive = true });
				builder.Entity<FirstTempInfo>().HasData(new FirstTempInfo() { Id = item, LangId = item, Description = "no desc", DarkImagePath = "no image", LightImagePath = "no image", IsActive = true });
				builder.Entity<SecondTempInfo>().HasData(new SecondTempInfo() { Id = item + 2, LangId = item, Description = "no desc", DarkImagePath = "no image", LightImagePath = "no image", IsActive = true });
			}
		}
	}
	public static class RegisterDbContext
	{
		static void AddDbContext(this IServiceCollection services, string connectionString)
		{
			services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
		}
	}
}

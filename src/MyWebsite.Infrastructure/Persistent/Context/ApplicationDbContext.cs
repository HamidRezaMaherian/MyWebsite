using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyWebsite.Domain.Entities;
using MyWebsite.Domain.Entities.Info;
using MyWebsite.Domain.Entities.User;

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

		#endregion

		public DbSet<SiteVisit> SiteVisits { get; set; }
		public DbSet<ActivityLog> ActivityLogs { get; set; }

		public void SeedData(ModelBuilder builder)
		{
			builder.Entity<Language>().HasData(
				SeedDataCreator.CreateLanguage()
				);
			builder.Entity<IdentityRole>().HasData(SeedDataCreator.CreateRole());
			builder.Entity<ApplicationUser>().HasData(SeedDataCreator.CreateDefaultUser());
			builder.Entity<IdentityUserRole<string>>().HasData(SeedDataCreator.CreateUserRole());
			builder.Entity<AboutMe>().HasData(SeedDataCreator.CreateAboutMe());
			builder.Entity<ContactMe>().HasData(SeedDataCreator.CreateContactMe());
			builder.Entity<MainInfo>().HasData(SeedDataCreator.CreateMainInfo());
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

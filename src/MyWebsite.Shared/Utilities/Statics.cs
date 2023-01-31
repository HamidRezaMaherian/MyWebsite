using MyWebsite.Shared.Resources;

namespace MyWebsite.Shared.Utilities
{
	public static class Statics
	{
		public static List<string> Months = new List<string>() {
		  SharedResource.Months_Farvardin,
		  SharedResource.Months_Ordibehesht,
		  SharedResource.Months_Khordad,
		  SharedResource.Months_Tir,
		  SharedResource.Months_Mordad,
		  SharedResource.Months_Shahrivar,
		  SharedResource.Months_Mehr,
		  SharedResource.Months_Aban,
		  SharedResource.Months_Azar,
		  SharedResource.Months_Dey,
		  SharedResource.Months_Bahman,
		  SharedResource.Months_Esfand,
		  };
		public static List<string> Days = new List<string>() {
		  SharedResource.Days_Sunday,
		  SharedResource.Days_Monday,
		  SharedResource.Days_Tuesday,
		  SharedResource.Days_Wednesday,
		  SharedResource.Days_Thursday,
		  SharedResource.Days_Friday,
		  SharedResource.Days_Saturday,
		  };
		public static class Roles
		{
			public const string SuperAdmin = "SuperAdmin";
			public const string CustomerUser = "CustomerUser";
		}
	}
}

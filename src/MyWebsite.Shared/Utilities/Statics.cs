using MyWebsite.Shared.Resources;
using System;
using System.Collections.Generic;
using System.Text;

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
		public static List<string> PageTypes = new List<string>()
		{
			"AboutUs",
			"ContactUs",
			"Honor",
			"Event",
			"ServiceConsultation",
			"Warrantyterm",
			"EducationalVideo",
			"ServiceAgencyRequest",
			"SaleAgencyRequest",
			"SaleConsultation",
			"Faq",
			"ClassSignUp",
			"Project",
			"Export",
			"Exhibition",
		};
		public static List<string> TicketStatusName = new List<string>()
		{
			"باز",
			"بسته",
		};
		public static class Roles
		{
			public const string SuperAdmin = "SuperAdmin";
			public const string CustomerUser = "CustomerUser";
		}
		public static class FileFormats
		{
			public static List<string> Video { get; set; } = new List<string>()
			{
				".avi",
				".divx",
				".flv",
				".m4v",
				".mkv",
				".mov",
				".mp4",
				".mpeg",
				".mpg",
				".ogm",
				".ogv",
				".ogx",
				".rm",
				".rmvb",
				".smil",
				".webm",
				".wmv",
				".xvid",
			};
			public static List<string> Image { get; set; } = new List<string>()
			{
				".jpeg",
				".jpg",
				".jpe",
				".gif",
				".png",
				".bmp",
				".ico",
				".svg",
				".svgz",
				".tif",
				".tiff",
				".ai",
				".drw",
				".pct",
				".psp",
				".xcf",
				".psd",
				".raw",
				".webp",
			};
		}
	}
}

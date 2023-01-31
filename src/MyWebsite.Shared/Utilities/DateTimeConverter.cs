using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebsite.Shared.Utilities
{
	public static class PersianDateTimeConverter
	{
		public static string ToShamsi(this DateTime dateTime)
		{
			//try
			//{
				var persianCalender = new PersianCalendar();
				return $"{persianCalender.GetYear(dateTime)}/{persianCalender.GetMonth(dateTime)}/{persianCalender.GetDayOfMonth(dateTime)}";
			//}
			//catch (Exception)
			//{
			//	return dateTime.ToString();
			//}
		}
		public static int GetShamsiDay(this DateTime dateTime)
		{
			var persianCalender = new PersianCalendar();
			return persianCalender.GetDayOfMonth(dateTime);
		}
		public static int GetShamsiMonth(this DateTime dateTime)
		{
			var persianCalender = new PersianCalendar();
			return persianCalender.GetMonth(dateTime);
		}
		public static int GetShamsiYear(this DateTime dateTime)
		{
			var persianCalender = new PersianCalendar();
			return persianCalender.GetYear(dateTime);
		}
		public static int GetShamsiDayOfWeek(this DateTime dateTime)
		{
			var persianCalender = new PersianCalendar();
			return (int)persianCalender.GetDayOfWeek(dateTime);
		}
	}
}

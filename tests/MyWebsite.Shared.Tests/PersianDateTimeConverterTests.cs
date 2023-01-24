using static MyWebsite.Shared.Utilities.PersianDateTimeConverter;

namespace MyWebsite.Shared.Tests
{
	[TestFixture]
	public class PersianDateTimeConverter
	{
		[Test]
		public void ToShamsi_CallWithInvalidDateTime()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() =>
			{
				var shamsiDateTime = new DateTime(1, 4, 1).ToShamsi();
			});
		}
		[Test]
		public void ToShamsi_CallWithValidDateTime()
		{
			var shamisDateTime = new DateTime(2023, 1, 24).ToShamsi();
			Assert.That(shamisDateTime, Is.EqualTo("1401/11/4"));
		}
		[Test]
		public void GetShamsiDay_CallWithInvalidDateTime()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() =>
			{
				var shamsiDateTime = new DateTime(1, 4, 1).GetShamsiDay();
			});
		}
		[Test]
		public void GetShamsiDay_CallWithValidDateTime()
		{
			var shamisDateTime = new DateTime(2023, 1, 24).GetShamsiDay();
			Assert.That(shamisDateTime, Is.EqualTo(4));
		}
		[Test]
		public void GetShamsiMonth_CallWithInvalidDateTime()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() =>
			{
				var shamsiDateTime = new DateTime(1, 4, 1).GetShamsiMonth();
			});
		}
		[Test]
		public void GetShamsiMonth_CallWithValidDateTime()
		{
			var shamisDateTime = new DateTime(2023, 1, 24).GetShamsiMonth();
			Assert.That(shamisDateTime, Is.EqualTo(11));
		}
		[Test]
		public void GetShamsiYear_CallWithInvalidDateTime()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() =>
			{
				var shamsiDateTime = new DateTime(1, 4, 1).GetShamsiYear();
			});
		}
		[Test]
		public void GetShamsiYear_CallWithValidDateTime()
		{
			var shamisDateTime = new DateTime(2023, 1, 24).GetShamsiYear();
			Assert.That(shamisDateTime, Is.EqualTo(1401));
		}
		[Test]
		public void GetShamsiDayOfWeek_CallWithValidDateTime()
		{
			var shamisDateTime = new DateTime(2023, 1, 24).GetShamsiDayOfWeek();
			Assert.That(shamisDateTime, Is.EqualTo(2));
		}
	}
}
using MyWebsite.Domain.Base;
using MyWebsite.Shared.Attributes;
using System.Net.Mail;

namespace MyWebsite.Domain.Entities.Info
{
	public class ContactMe : BaseLanguage
	{
		[Required]
		[EmailAddress]
		[StringLength(50)]
		public string Email { get; set; }
		[Phone]
		[Required]
		[StringLength(11)]
		public string PhoneNumber { get; set; }
		[Url]
		[StringLength(150)]
		public string Instagram { get; set; }
		[Url]
		[StringLength(150)]
		public string Linkedin { get; set; }
		[StringLength(150)]
		[Url]
		public string Telegram { get; set; }
		[StringLength(150)]
		public string WhatsApp { get; set; }
		[StringLength(150)]
		public string Twitter { get; set; }
		[StringLength(150)]
		public string FaceBook { get; set; }

		public override bool Equals(object obj)
		{
			var contactMeObj = obj as ContactMe;
			return
				Email == contactMeObj?.Email &&
				PhoneNumber == contactMeObj?.PhoneNumber &&
				Instagram == contactMeObj?.Instagram &&
				Linkedin == contactMeObj?.Linkedin &&
				Telegram == contactMeObj?.Telegram &&
				WhatsApp == contactMeObj?.WhatsApp &&
				Twitter == contactMeObj?.Twitter &&
				FaceBook == contactMeObj?.FaceBook &&
				Id == contactMeObj?.Id &&
				LangId == contactMeObj?.LangId;
		}
	}
}

using MyWebsite.Domain.Models.Base;
using MyWebsite.Domain.Models.User;
using MyWebsite.Shared.Attributes;

namespace MyWebsite.Domain.Models.ClientUI
{
	public class ContactUsForm : BaseEntity
	{
		public string UserId { get; set; }

		[Required]
		public string Name { get; set; }
		[EmailAddress]
		[Required]
		public string Email { get; set; }
		[Required]
		public string Subject { get; set; }

		[Required]
		public string Message { get; set; }

		[MaxLength(500)]
		public string Answer { get; set; }

		public bool IsAnswered { get; set; }
		public DateTime QuestionDateTime { get; set; }
		public DateTime AnswerDateTime { get; set; }

		#region NavigationProps
		public virtual ApplicationUser User { get; set; }
		#endregion
	}
}

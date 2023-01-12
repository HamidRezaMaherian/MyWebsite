using MyWebsite.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace MyWebsite.Domain.Models.Job
{
   public class Order : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string PersonName { get; set; }
        [MaxLength(200)]
        public string CompanyName { get; set; }
        [MaxLength(200)]
        public string Email { get; set; }
        [Required]
        [MaxLength(100)]
        public string PhoneNumber { get; set; }
        [MaxLength(100)]
        public string LandLinePhoneNumber { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public string FilePath { get; set; }
        [Required]
        public DateTime RequestDateTime { get; set; }
        public DateTime AnswerDateTime { get; set; }
        public bool IsAnswered { get; set; }
    }
}

using MyWebsite.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace MyWebsite.Domain.Entities
{
   public class SiteVisit : BaseEntity
   {
      [Required]
      public string IP { get; set; }
      public string UrlReferer { get; set; }
      public string EndPoint { get; set; }

      public byte Day { get; set; }
      public byte Month { get; set; }
      public ushort Year { get; set; }


      [Required]
      public DateTime DateTime { get; set; }
   }
}

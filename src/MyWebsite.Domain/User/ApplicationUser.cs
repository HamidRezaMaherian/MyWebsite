using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyWebsite.Domain.Models.Base;
using System.Globalization;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Domain.Models.User
{
   public class ApplicationUser : IdentityUser
   {
      public DateTime SignUpDateTime { get; set; }
      public DateTime LastLoginDateTime { get; set; }
      public string ImagePath { get; set; }
      public string FullName { get; set; }
      [Display(Name = "وضعیت نمایش")]
      public bool IsActive { get; set; }
      public bool IsDelete { get; set; }
   }
}

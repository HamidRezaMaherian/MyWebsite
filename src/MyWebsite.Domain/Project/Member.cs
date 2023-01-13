﻿using MyWebsite.Domain.Models.Base;
using MyWebsite.Shared.Attributes;

namespace MyWebsite.Domain.Models.Projects
{
   public class Member : BaseLanguage
   {
      [StringLength(150)]
      public string Name { get; set; }
      [StringLength(200)]
      public string Role { get; set; }
      public string ImagePath { get; set; }
      #region SocialMedia
      public string Email { get; set; }
      #endregion
   }
}

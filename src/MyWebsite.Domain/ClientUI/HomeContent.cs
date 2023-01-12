﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyWebsite.Domain.Models.Base;

namespace MyWebsite.Domain.Models.ClientUI
{
   public class HomeContent : BaseLanguage
   {
      public string Title { get; set; }
      public string PageType { get; set; }
      public string ImagePath { get; set; }
   }
}

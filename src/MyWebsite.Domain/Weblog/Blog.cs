using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyWebsite.Domain.Models.Base;

namespace MyWebsite.Domain.Models.Weblog
{
    public class Blog : BaseDescribe
    {
        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string ImagePath { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Tags { get; set; }

        public int Visit { get; set; }

        #region NavigationProps

        public virtual ICollection<BlogComment> BlogComments { get; set; }

        #endregion
    }
}

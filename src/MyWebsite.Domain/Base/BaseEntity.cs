using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyWebsite.Domain.Models.Base
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="وضعیت نمایش")]
        public bool IsActive { get; set; }
        [Display(Name ="وضعیت حذف")]
        public bool IsDelete { get; set; }
    }
}

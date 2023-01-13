using MyWebsite.Shared.Resources;
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
		[Display(ResourceType = typeof(DomainResource))]
		public bool IsActive { get; set; }
		[Display(ResourceType = typeof(DomainResource))]
		public bool IsDelete { get; set; }
	}
}

﻿using MyWebsite.Domain.Base;
using MyWebsite.Shared.Attributes;

namespace MyWebsite.Domain.Entities.Info
{
	public class AboutMe : BaseLanguage
	{
		[Required]
		public string FilePath { get; set; }
	}
	public class AboutMeKeyValue : BaseLanguage
	{
		public string Key { get; set; }
		public string Value { get; set; }
	}
}
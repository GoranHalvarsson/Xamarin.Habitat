

namespace HabitatApp.Models
{
	using System;
	using SQLite.Net.Attributes;

	public class CachedData
	{
		public Guid Id{ get; internal set; }

		[PrimaryKey]
		[MaxLength(1024)]
		public string Url { get; internal set; }

		public DateTime? UpdatedAt { get; internal set;}
	}
}


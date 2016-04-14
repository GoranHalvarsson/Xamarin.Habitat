namespace HabitatApp.Models
{
	using System;
	using SQLite.Net.Attributes;

	public class CachedMedia : CachedData
	{
		[MaxLength(123456789)]
		public byte[] MediaData { get; internal set; }

		public string MediaType { get; internal set; }
	}
}


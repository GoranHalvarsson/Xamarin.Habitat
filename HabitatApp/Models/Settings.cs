namespace HabitatApp.Models
{
	using System;
	using SQLite.Net.Attributes;

	public class Settings
	{
		[PrimaryKey]
		public string RestBaseUrl {
			get;
			set;
		}

		public string SitecoreNavigationRootPath {
			get;
			set;
		}

		public string SitecoreNavigationRootId {
			get;
			set;
		}

		public string SitecoreUserName {
			get;
			set;
		}

		public string SitecorePassword {
			get;
			set;
		}

		public string SitecoreShellSite {
			get;
			 set;
		}

		public string SitecoreDefaultDatabase {
			get;
			 set;
		}

		public string SitecoreDefaultLanguage {
			get;
			 set;
		}

		public string SitecoreMediaLibraryRoot {
			get;
			 set;
		}

		public string SitecoreMediaPrefix {
			get;
			 set;
		}

		public string SitecoreDefaultMediaResourceExtension {
			get;
			 set;
		}

		public int CachingInHours {
			get;
			 set;
		}


	}
}


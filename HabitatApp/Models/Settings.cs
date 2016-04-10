namespace HabitatApp.Models
{
	using System;
	using SQLite.Net.Attributes;

	public class Settings
	{
		[PrimaryKey]
		public string RestBaseUrl {
			get;
			internal set;
		}

		public string SitecoreNavigationRootPath {
			get;
			internal set;
		}

		public string SitecoreNavigationRootId {
			get;
			internal set;
		}

		public string SitecoreUserName {
			get;
			internal set;
		}

		public string SitecorePassword {
			get;
			internal set;
		}

		public string SitecoreShellSite {
			get;
			internal set;
		}

		public string SitecoreDefaultDatabase {
			get;
			internal set;
		}

		public string SitecoreDefaultLanguage {
			get;
			internal set;
		}

		public string SitecoreMediaLibraryRoot {
			get;
			internal set;
		}

		public string SitecoreMediaPrefix {
			get;
			internal set;
		}

		public string SitecoreDefaultMediaResourceExtension {
			get;
			internal set;
		}
	}
}


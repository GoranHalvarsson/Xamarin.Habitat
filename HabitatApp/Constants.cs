namespace HabitatApp
{
	public static class Constants
	{
		public struct Sitecore
		{
			public struct Templates
			{
				public const string StartPage = "{717D98BE-18EA-4F2D-87B5-1205D49C6A08}";
				public const string CarouselParentPage = "{C2729CB5-650C-4EFF-B255-5060A7DCC20B}";
				public const string ListParentPage = "{8FBEEA06-69FC-4919-B711-42384D49FA28}";
				public const string SimpleContentPage = "{B8EEF141-085B-46CE-875F-ECFFD01EA2C8}";
			}


			public struct Fields
			{

				public struct Teasers
				{
					public const string TeaserSelector = "TeaserSelector";
					public const string TeaserTitle = "TeaserTitle";
					public const string TeaserImage = "TeaserImage";
					public const string TeaserSummary = "TeaserSummary";
					public const string TeaserLink = "TeaserLink";

				}

				public struct Navigation
				{
					public const string NavigationTitle = "NavigationTitle";
					public const string ShowInNavigation = "ShowInNavigation";
					public const string Icon = "Icon";
				}

				public struct PageContent
				{
					public const string Title = "Title";
					public const string Image = "Image";
					public const string Summary = "Summary";
					public const string Body = "Body";
				}

			}
		}
	}
}


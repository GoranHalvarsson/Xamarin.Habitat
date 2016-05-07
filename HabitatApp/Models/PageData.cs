namespace HabitatApp.Models
{
	using System;
	using System.Collections.Generic;
	using Sitecore.MobileSDK.API.Items;

	public class PageData
	{
		public string PageName { get; set; }

		public string PageType { get; set; }

		public string NavigationTitle { get; set; }

		public ScItemsResponse ItemContext { get; set; }

		public IList<ScItemsResponse> DataSourceFromField { get; set; }

		public ScItemsResponse DataSourceFromChildren { get; set; }

	}
}


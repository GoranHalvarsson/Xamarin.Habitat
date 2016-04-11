

namespace HabitatApp.Services
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Sitecore.MobileSDK.API.Items;
	using HabitatApp.Models;

	public interface IListItemService
	{
		Task<IEnumerable<ListItem>> GenerateListItemsFromTeasers (IList<ScItemsResponse> itemsResponse);
		Task<IEnumerable<ListItem>> GenerateListItemsFromChildren (ScItemsResponse itemsResponse);

		Task<ListItem> GetDefaultListItem ();
	}
}





namespace HabitatApp.Services
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Sitecore.MobileSDK.API.Items;
	using HabitatApp.Models;
	using HabitatApp.Extensions;
	using System;
	using System.Linq;
	using HabitatApp.Repositories;

	public class ListItemService : IListItemService
	{

		private readonly ILoggingService _loggingService;
		private readonly ICachedMediaRepository _cachedMediaRepository;

		public ListItemService (ICachedMediaRepository cachedMediaRepository, ILoggingService loggingService)
		{
			_cachedMediaRepository = cachedMediaRepository;
			_loggingService = loggingService;
		}

		public async Task<IEnumerable<ListItem>> GenerateListItemsFromTeasers(IList<ScItemsResponse> itemsResponse)
		{

			List<ListItem> list = new List<ListItem> ();

				
			for (int i = 0; i < itemsResponse.Count(); i++) {


				if (itemsResponse [i] == null || itemsResponse [i].ResultCount == 0)
					continue;

				ISitecoreItem item = itemsResponse [i].First ();

				ListItem listlItem = new ListItem{ 
					Header = item.GetValueFromField(Constants.Sitecore.Fields.Teasers.TeaserTitle),
					Text = item.GetValueFromField(Constants.Sitecore.Fields.Teasers.TeaserSummary),
					NavigationItem = item.GetItemIdFromLinkField(Constants.Sitecore.Fields.Teasers.TeaserLink),
					NavigationText = item.GetTextFromLinkField(Constants.Sitecore.Fields.Teasers.TeaserLink),
					Media =  await _cachedMediaRepository.GetCache(item.GetImageUrlFromMediaField(Constants.Sitecore.Fields.Teasers.TeaserImage))	


				};


				list.Add (listlItem);

			}

			return list;

		}

		public async Task<IEnumerable<ListItem>> GenerateListItemsFromChildren(ScItemsResponse itemsResponse)
		{

			List<ListItem> list = new List<ListItem> ();


			for (int i = 0; i < itemsResponse.ResultCount; i++) {

				ISitecoreItem item = itemsResponse[i];

				if (item == null)
					continue;

				ListItem listlItem = new ListItem{ 
					Header = item.GetValueFromField (Constants.Sitecore.Fields.PageContent.Title),
					Text = item.GetValueFromField (Constants.Sitecore.Fields.PageContent.Summary),
					NavigationItem = item.Id,
					NavigationText = item.GetValueFromField (Constants.Sitecore.Fields.PageContent.Title),
					SitecoreItem = item,
					Media =  await _cachedMediaRepository.GetCache(item.GetImageUrlFromMediaField(Constants.Sitecore.Fields.PageContent.Image))	
	
				};

				list.Add (listlItem);

			}

			return list;

		}



	}
}


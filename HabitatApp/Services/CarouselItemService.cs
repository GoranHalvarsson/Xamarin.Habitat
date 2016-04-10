
namespace HabitatApp.Services.Cache
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Sitecore.MobileSDK.API.Items;
	using HabitatApp.Models;
	using HabitatApp.Extensions;
	using System.Linq;

	public class CarouselItemService : ICarouselItemService
	{

		public async Task<IEnumerable<CarouselItem>> GenerateCarouselItemsFromTeasers(IList<ScItemsResponse> itemsResponse)
		{

			List<CarouselItem> list = new List<CarouselItem> ();

				
			for (int i = 0; i < itemsResponse.Count(); i++) {


				if (itemsResponse [i] == null || itemsResponse [i].ResultCount == 0)
					continue;

				ISitecoreItem item = itemsResponse [i].First ();

				CarouselItem carouselItem = new CarouselItem{ 
					Header = item.GetValueFromField(Constants.Sitecore.Fields.Teasers.TeaserTitle),
					Text = item.GetValueFromField(Constants.Sitecore.Fields.Teasers.TeaserSummary),
					NavigationItem = item.GetItemIdFromLinkField(Constants.Sitecore.Fields.Teasers.TeaserLink),
					NavigationText = item.GetTextFromLinkField(Constants.Sitecore.Fields.Teasers.TeaserLink),
					CarouselImage = item.GetImageUrlFromMediaField(Constants.Sitecore.Fields.Teasers.TeaserImage)	


				};


				list.Add (carouselItem);

			}

			return list;

		}

		public async Task<IEnumerable<CarouselItem>> GenerateCarouselItemsFromChildren(ScItemsResponse itemsResponse)
		{

			List<CarouselItem> list = new List<CarouselItem> ();


			for (int i = 0; i < itemsResponse.ResultCount; i++) {

				ISitecoreItem item = itemsResponse[i];

				if (item == null)
					continue;

				CarouselItem carouselItem = new CarouselItem { 
					Header = item.GetValueFromField (Constants.Sitecore.Fields.PageContent.Title),
					Text = item.GetValueFromField (Constants.Sitecore.Fields.PageContent.Summary),
					NavigationItem = item.Id,
					NavigationText = item.GetValueFromField (Constants.Sitecore.Fields.PageContent.Title),
					CarouselImage = item.GetImageUrlFromMediaField (Constants.Sitecore.Fields.PageContent.Image)	

				};

				list.Add (carouselItem);

			}

			return list;

		}


		public async Task<CarouselItem> GetDefaultCarouselItem ()
		{
			return new CarouselItem{
				Header = "Extensibility",
				Text = "A consistent and discoverable architecture",
				CarouselImage = "http://habitat.sitecore.net/-/media/Habitat/Images/Wide/Habitat-001-wide.jpg"	

			};
		}


	}
}


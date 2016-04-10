

namespace HabitatApp.Services
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Sitecore.MobileSDK.API.Items;
	using HabitatApp.Models;

	public interface ICarouselItemService
	{
		Task<IEnumerable<CarouselItem>> GenerateCarouselItemsFromTeasers (IList<ScItemsResponse> itemsResponse);
		Task<IEnumerable<CarouselItem>> GenerateCarouselItemsFromChildren (ScItemsResponse itemsResponse);

		Task<CarouselItem> GetDefaultCarouselItem ();
	}
}


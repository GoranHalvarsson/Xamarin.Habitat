

namespace HabitatApp.Services
{

	using System.Collections.Generic;
	using System.Threading.Tasks;
	using System.Linq;
	using Xamarin.Forms;
	using Sitecore.MobileSDK.API.Request.Parameters;
	using Sitecore.MobileSDK.API.Items;
	using HabitatApp.Models;
	using HabitatApp.Repositories;
	using HabitatApp.Extensions;

	public class NavigationMenuService : ObservableModel, INavigationMenuService 
	{

		private readonly ISitecoreService _sitecoreService;
		private readonly ISettingsRepository _settingsRepository;

		public NavigationMenuService (ISitecoreService sitecoreService, ISettingsRepository settingsRepository)
		{
			_sitecoreService = sitecoreService;
			_settingsRepository = settingsRepository;
		}


		public async Task<IEnumerable<NavigationMenuItem>> GenerateMenuItems(){

			Settings settings = await _settingsRepository.GetWithFallback ();

			PageData rootPageData = await _sitecoreService.GeneratePageData (
				settings.SitecoreNavigationRootId, 
				PayloadType.Content, 
				new List<ScopeType> () {
					ScopeType.Self,
					ScopeType.Children
				}, 
				Constants.Sitecore.Fields.Teasers.AccordeonSelector, 
				settings.SitecoreDefaultLanguage);

			if (rootPageData == null || rootPageData.ItemContext == null)
				return null;

			List<NavigationMenuItem> menuItems = new List<NavigationMenuItem> ();

			for (int i = 0; i < rootPageData.ItemContext.ResultCount; i++) {

				ISitecoreItem item = rootPageData.ItemContext [i];

				if (item == null)
					continue;

				NavigationMenuItem menuItem = new NavigationMenuItem {
					Title = item.GetValueFromField(Constants.Sitecore.Fields.Navigation.NavigationTitle),
					RowColor = Color.Transparent,
					PageContext = await GetPageData(item, rootPageData),
					ShowInMenu = item.GetCheckBoxValueFromField(Constants.Sitecore.Fields.Navigation.ShowInNavigation)
				};

				menuItems.Add(menuItem);

			}

			return menuItems;

		}

		private async Task<PageData> GetPageData(ISitecoreItem currentItem, PageData rootPageData){

			//If its root we don't need to fetch it again
			if (currentItem.Id == rootPageData.ItemContext.First ().Id)
				return rootPageData;

			return await _sitecoreService.GeneratePageData (currentItem.Id, PayloadType.Content, new List<ScopeType> (){ ScopeType.Self }, Constants.Sitecore.Fields.Teasers.AccordeonSelector, currentItem.Source.Language);


		}

	}
}




namespace HabitatApp.Services
{
	using System;
	using Xamarin.Forms;
	using System.Threading.Tasks;
	using Sitecore.MobileSDK.API.Items;
	using System.Linq;
	using Sitecore.MobileSDK.API.Request.Parameters;
	using System.Collections.Generic;
	using HabitatApp.Repositories;
	using HabitatApp.Models;
	using HabitatApp.Extensions;
	using HabitatApp.ViewModels;

	public class NavigationService : INavigationService
	{

		private readonly ISitecoreService _sitecoreService;
		private readonly ISettingsRepository _settingsRepository;

		public NavigationService (ISitecoreService sitecoreService, ISettingsRepository settingsRepository)
		{
			_sitecoreService = sitecoreService;
			_settingsRepository = settingsRepository;
		}

		private async Task<Page> LoadPageByPageData(PageData pageData)
		{

			if (pageData == null || pageData.ItemContext == null)
				return null;

			ISitecoreItem item = pageData.ItemContext.First ();

			if(item == null)
				return null;


			Type pageType = Type.GetType (string.Format("HabitatApp.Views.Pages.{0}",item.GetTemplateName ()));

			if (pageType == null)
				return null;

			//Load page by page type
			Page currentPage = (Page)Activator.CreateInstance (pageType);

			((IViewModel)currentPage.BindingContext).PageContext = pageData;

			((IViewModel)currentPage.BindingContext).ConnectedToPage = currentPage;

			return currentPage;

		}

		private async Task<Page> LoadPageByItemId(string id)
		{

			Settings settings = await _settingsRepository.GetWithFallback ();

			PageData pageData = await _sitecoreService.GeneratePageData(id, 
				PayloadType.Content, 
				new List<ScopeType>(){ ScopeType.Self }, 
				Constants.Sitecore.Fields.Teasers.AccordeonSelector, 
				settings.SitecoreDefaultLanguage);


			return await LoadPageByPageData(pageData);

		}


		public async Task NavigateToPageByItemId (Page navigateFromPage, string itemId)
		{
			Page page = await LoadPageByItemId(itemId);
			await NavigateAndLoadBindingContext (navigateFromPage, page);
		}

		public async Task NavigateToPageByPageData (Page navigateFromPage, PageData pageData)
		{
			Page page = await LoadPageByPageData(pageData);
			await NavigateAndLoadBindingContext (navigateFromPage, page);
		}

	

		private async Task NavigateAndLoadBindingContext(Page navigateFromPage, Page navigateToPage){

			IViewModel viewModel = (IViewModel)navigateToPage.BindingContext;

			//We need to load it all before page appears
			//Page.Appering() gives an unfortunate delay
			await viewModel.LoadAsync ();

			//navigateToPage.Appearing += async (object sender, EventArgs e) => await viewModel.LoadAsync ();

			//navigateToPage.Disappearing += async (object sender, EventArgs e) => await viewModel.UnLoadAsync ();


			await navigateFromPage.Navigation.PushAsync (navigateToPage);
		}



	}
}


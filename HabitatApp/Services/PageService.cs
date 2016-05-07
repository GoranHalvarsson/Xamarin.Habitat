

namespace HabitatApp.Services
{

	using System;
	using HabitatApp.Repositories;
	using Xamarin.Forms;
	using System.Threading.Tasks;
	using HabitatApp.Models;
	using System.Collections.Generic;
	using Sitecore.MobileSDK.API.Request.Parameters;
	using HabitatApp.ViewModels;
	using Sitecore.MobileSDK.API.Items;
	using Autofac;
	using System.Linq;
	using HabitatApp.Exceptions.Page;
	using HabitatApp.Extensions;

	public class PageService : IPageService
	{
		private readonly ISitecoreService _sitecoreService;
		private readonly ISettingsRepository _settingsRepository;
		private ILoggingService _loggingService;

		public PageService (ISitecoreService sitecoreService, ISettingsRepository settingsRepository, ILoggingService loggingService)
		{
			_sitecoreService = sitecoreService;
			_settingsRepository = settingsRepository;
			_loggingService = loggingService;
		}

		public Task<Page> LoadPageByPageData (PageData pageData)
		{

			TaskCompletionSource<Page> taskCompletions = new TaskCompletionSource<Page> ();

			Task.Run (() => {
			
				if (pageData == null || pageData.ItemContext == null) {
					taskCompletions.SetException(new PageFailedException ("PageData or ItemContext is empty"));
				}
				
				ISitecoreItem item = pageData.ItemContext.First ();

				if (item == null) {
					string message = $"Sitecore Item is null for page {pageData.PageName}";
					taskCompletions.SetException(new PageFailedException (pageData.PageName, message));
				}
				

				Type pageType = Type.GetType ($"HabitatApp.Views.Pages.{item.GetTemplateName()}");

				if (pageType == null) {
					string message = $"Could not identify pagetype for page name {pageData.PageName}";
					taskCompletions.SetException(new PageFailedException (pageData.PageName, message));
				}


				try {
					//Load page by page type
					Page currentPage = App.AppInstance.Container.Resolve (pageType) as Page;

					IViewModel viewModel = (IViewModel)currentPage.BindingContext;

					viewModel.PageContext = pageData;

					taskCompletions.SetResult (currentPage);

				} catch (Exception ex) {
					string message = $"Could not resolve page for page name {pageData.PageName}";
					taskCompletions.SetException(new PageFailedException (pageData.PageName, message, ex));
				}

			});

			return taskCompletions.Task;

		}

		public async Task<Page> LoadPageByItemId (string id)
		{

			Settings settings = await _settingsRepository.GetWithFallback ();

			PageData pageData = await _sitecoreService.GeneratePageData (id, 
				                    PayloadType.Content, 
				                    new List<ScopeType> (){ ScopeType.Self }, 
				                    Constants.Sitecore.Fields.Teasers.TeaserSelector, 
				                    settings.SitecoreDefaultLanguage);


			return await LoadPageByPageData (pageData);

		}
	}
}


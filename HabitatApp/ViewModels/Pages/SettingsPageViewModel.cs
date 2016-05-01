using Sitecore.MobileSDK.API.Items;
using System.Linq;
using HabitatApp.Extensions;


namespace HabitatApp.ViewModels.Pages
{
	using System;

	using System.Threading.Tasks;
	using HabitatApp.Models;
	using HabitatApp.Repositories;

	public class SettingsPageViewModel: ViewModelBase
	{
		private readonly ICachedMediaRepository _cachedMediaRepository;
		private readonly ISettingsRepository _settingsRepository;

		public SettingsPageViewModel (ICachedMediaRepository cachedMediaRepository, ISettingsRepository settingsRepository)
		{
			_cachedMediaRepository = cachedMediaRepository;
			_settingsRepository = settingsRepository;
		}

		private Settings _userSettings;

		public Settings UserSettings {
			get {
				return _userSettings;
			}
			set { 
				SetProperty (ref _userSettings, value); 
			}

		}

		private string _contentHeader = string.Empty;

		public string ContentHeader {
			get {
				return _contentHeader;
			}
			set { SetProperty (ref _contentHeader, value); }
		}

		private string _contentSummary = string.Empty;

		public string ContentSummary {
			get {
				return _contentSummary;
			}
			set { SetProperty (ref _contentSummary, value); }
		}

		/// <summary>
		/// Entering page, lets load data
		/// </summary>
		/// <returns>The async.</returns>
		public async override Task LoadAsync ()
		{

			SetBusy ("Loading");

			await SetData (base.PageContext);

			ClearBusy ();

		}

		/// <summary>
		/// When user leaves pages we will save the changes.
		/// </summary>
		/// <returns>The load async.</returns>
		public async override Task UnLoadAsync(){
			
			await _settingsRepository.Update (UserSettings);
		
		}
	

		private async Task SetData(PageData pageData){

			base.Title = "Settings";

			UserSettings = await _settingsRepository.GetWithFallback ();

			base.Title = pageData.NavigationTitle;

			ISitecoreItem item = pageData.ItemContext.FirstOrDefault();

			if (item == null)
				return;

			ContentHeader = item.GetValueFromField(Constants.Sitecore.Fields.PageContent.Title);
			ContentSummary = item.GetValueFromField(Constants.Sitecore.Fields.PageContent.Summary);


		} 


	}
}


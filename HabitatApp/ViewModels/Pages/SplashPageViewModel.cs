

namespace HabitatApp.ViewModels.Pages
{

	using System;
	using Xamarin.Forms;
	using System.Threading.Tasks;
	using HabitatApp.Models;
	using HabitatApp.Views.Pages;
	using System.Windows.Input;
	using Plugin.Connectivity;
	using HabitatApp.Repositories;
	using Autofac;


	public class SplashPageViewModel : ViewModelBase
	{
		private readonly ISettingsRepository _settingsRepository;

		public SplashPageViewModel (ISettingsRepository settingsRepository)
		{
			_settingsRepository = settingsRepository;
		}


		private string _habitatLogo;

		public String HabitatLogo {
			get {
				return _habitatLogo;
			}
			set { SetProperty (ref _habitatLogo, value); }

		}

		private bool _connectionOk = true;

		public bool ConnectionOk {
			get {
				return _connectionOk;
			}
			set { SetProperty (ref _connectionOk, value); }

		}


		private Command _reStartCommand;
		public ICommand ReStartCommand
		{
			get
			{
				if (_reStartCommand == null)
				{
					_reStartCommand = new Command (async (parameter) => await SetData ());
				}

				return _reStartCommand;
			}
		}


		/// <summary>
		/// Loads the async.
		/// </summary>
		public async override Task LoadAsync()
		{

			HabitatLogo = "HabitatSmallTransparent.png"; 

			await SetData ();

		}

		/// <summary>
		/// Sets the data.
		/// </summary>
		/// <returns>The data.</returns>
		private async Task SetData(){
		
			ConnectionOk = await NetworkAndSiteIsAvailable();

			if (!ConnectionOk)
				return;

			HandleNavigation ();

		} 

		/// <summary>
		/// Check if network and site is available.
		/// </summary>
		private async Task<bool> NetworkAndSiteIsAvailable ()
		{

			if (!CrossConnectivity.Current.IsConnected) {
				await App.AppInstance.MainPage.DisplayAlert ("Network Issues", "Some issues with network", "Close");
				return false;
			}

			Settings settings = await _settingsRepository.GetWithFallback ();

			bool siteIsUp = await CrossConnectivity.Current.IsRemoteReachable (settings.RestBaseUrl);

			if (!siteIsUp) {
				await PopSettingsAsModal();
				return false;
			}

			return true;
		}



		/// <summary>
		/// Handles the navigation.
		/// </summary>
		private void HandleNavigation ()
		{

			using (ILifetimeScope lifetimeScope = HabitatApp.App.AppInstance.Container.BeginLifetimeScope())
			{

				if (Device.OS == TargetPlatform.iOS) {
					Application.Current.MainPage = lifetimeScope.Resolve<NavigationTabbedPage> ();
				} else {
					Application.Current.MainPage = lifetimeScope.Resolve<NavigationMasterPage> ();
				}
	
			}


		}

		/// <summary>
		/// Pops the settings as modal.
		/// </summary>
		/// <returns>The settings as modal.</returns>
		private async Task PopSettingsAsModal ()
		{
			Type pageType = Type.GetType ("HabitatApp.Views.Pages.SettingsPage");

			Page modalPage = App.AppInstance.Container.Resolve (pageType) as Page;
			SettingsPageViewModel viewModel = (SettingsPageViewModel)modalPage.BindingContext;
			viewModel.ConnectedToPage = modalPage;

			//We need to load it all before page appears
			//Page.Appering() gives an unfortunate delay
			await viewModel.LoadAsync ();

			viewModel.ContentSummary = "Please enter a valid website url";

			modalPage.ToolbarItems.Add (new ToolbarItem {
				Text = "Close",
				Command = new Command (() => ConnectedToPage.Navigation.PopModalAsync ())
			} );
			NavigationPage nav = new NavigationPage (modalPage);
			nav.BarBackgroundColor = Color.FromRgb (46, 56, 78);
			nav.BarTextColor = Color.White;
			await ConnectedToPage.Navigation.PushModalAsync (nav);
		}

	}
}


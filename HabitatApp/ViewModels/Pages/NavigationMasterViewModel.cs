namespace HabitatApp.ViewModels.Pages
{

	using System;
	using Xamarin.Forms;
	using System.Collections.ObjectModel;
	using System.Threading.Tasks;
	using System.Collections.Generic;
	using System.Linq;
	using HabitatApp.Services;
	using HabitatApp.Models;
	using HabitatApp.Extensions;
	using HabitatApp.Views.Pages;
	using System.Windows.Input;
	using Plugin.Connectivity;
	using HabitatApp.Repositories;


	public class NavigationMasterViewModel : ViewModelBase
	{
		private readonly INavigationMenuService _navigationMenuService;
		private readonly INavigationService _navigationService;
		private readonly ISettingsRepository _settingsRepository;

		public NavigationMasterViewModel (INavigationService navigationService, INavigationMenuService navigationMenuService, ISettingsRepository settingsRepository)
		{
			_navigationMenuService = navigationMenuService;
			_navigationService = navigationService;
			_settingsRepository = settingsRepository;
		}

		private ObservableCollection<NavigationMenuItem> _menuItems = new ObservableCollection<NavigationMenuItem> ();

		public ObservableCollection<NavigationMenuItem> MenuItems {
			get {
				return _menuItems;
			}
			set { SetProperty (ref _menuItems, value); }
		}


		private NavigationMenuItem _menuItemSelected;

		public NavigationMenuItem MenuItemSelected {
			get {
				return _menuItemSelected;
			}
			set {
				SetProperty (ref _menuItemSelected, value);

				if (_menuItemSelected != null) {

					HandleRowStyleForSelectedMenuItem (_menuItemSelected);

					HandleNavigationForSelectedMenuItem (_menuItemSelected);
				}

			}

		}

		private string _habitatLogo;

		public String HabitatLogo {
			get {
				return _habitatLogo;
			}
			set { SetProperty (ref _habitatLogo, value); }

		}

		private string _menuIcon;

		public String MenuIcon {
			get {
				return _menuIcon;
			}
			set { SetProperty (ref _menuIcon, value); }

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
		/// <returns>The async.</returns>
		public async override Task LoadAsync()
		{

			HabitatLogo = "HabitatSmallTransparent.png"; 


			await SetData ();

		}


		private async Task<bool> NewtWorkAndSiteIsAvailable ()
		{
			

			if (!CrossConnectivity.Current.IsConnected) {
				await App.AppInstance.MainPage.DisplayAlert ("Network Issues", "Some issues with network", "Close");
				return false;
			}

			Settings settings = await _settingsRepository.GetWithFallback ();

			bool siteIsUp = await CrossConnectivity.Current.IsRemoteReachable (settings.RestBaseUrl);

			if (!siteIsUp) {
				await App.AppInstance.MainPage.DisplayAlert("Website issues", "Website is not reachable", "Close");
				return false;
			}

			return true;
		}

		private async Task SetData(){
		
			ConnectionOk = await NewtWorkAndSiteIsAvailable();

			if (!ConnectionOk)
				return;


			IEnumerable<NavigationMenuItem> menuItems = await _navigationMenuService.GenerateMenuItems ();

			if (menuItems == null)
				return;

			MenuItems = menuItems.Where(item => item.ShowInMenu).ToObservableCollection();

			if(MenuItems.Any())
				MenuItemSelected = MenuItems.ElementAt (0);

			MenuIcon = "HamburgerIcon.png";

		} 

		/// <summary>
		/// Sets the menu.
		/// </summary>
		/// <returns>The menu.</returns>
		private async Task SetMenu ()
		{
			IEnumerable<NavigationMenuItem> menuItems = await _navigationMenuService.GenerateMenuItems ();
			if (menuItems == null)
				return;

			MenuItems = menuItems.Where(item => item.ShowInMenu).ToObservableCollection();
		}


		/// <summary>
		/// Handles the navigation for selected menu item.
		/// </summary>
		/// <param name="navigationMenuItem">Navigation menu item.</param>
		private async void HandleNavigationForSelectedMenuItem (NavigationMenuItem navigationMenuItem)
		{
			//This is ugly need to clean it up
			NavigationPage nav = new NavigationPage();
			nav.BarBackgroundColor = Color.FromRgb(46, 56, 78);
			nav.BarTextColor = Color.White; 

			await _navigationService.NavigateToPageByPageData (nav, navigationMenuItem.PageContext);

			((NavigationMasterPage)ConnectedToPage).IsPresented = false;
			((NavigationMasterPage)ConnectedToPage).Detail = nav;


		}

		/// <summary>
		/// Handles the row style for selected menu item.
		/// </summary>
		/// <param name="navigationMenuItem">Navigation menu item.</param>
		private void HandleRowStyleForSelectedMenuItem (NavigationMenuItem navigationMenuItem)
		{
			NavigationMenuItem previousSelectedItem =  MenuItems.FirstOrDefault(m => m.RowColor == Color.Red);

			if(previousSelectedItem!=null)
				previousSelectedItem.RowColor = Color.Transparent;

			NavigationMenuItem currentSelectedItem =  MenuItems.FirstOrDefault(m => m.Title == MenuItemSelected.Title);
			currentSelectedItem.RowColor = Color.Red;

		}

	}
}


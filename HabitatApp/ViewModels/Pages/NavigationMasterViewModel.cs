

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

	public class NavigationMasterViewModel : ViewModelBase
	{
		private readonly INavigationMenuService _navigationMenuService;
		private readonly INavigationService _navigationService;

		public NavigationMasterViewModel (INavigationService navigationService, INavigationMenuService navigationMenuService)
		{
			_navigationMenuService = navigationMenuService;
			_navigationService = navigationService;

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


		private string _menuIcon;

		public String MenuIcon {
			get {
				return _menuIcon;
			}
			set { SetProperty (ref _menuIcon, value); }

		}

		/// <summary>
		/// Loads the async.
		/// </summary>
		/// <returns>The async.</returns>
		public async override Task LoadAsync()
		{
			await SetMenu ();

			MenuIcon = "HamburgerIcon.png";

			if(MenuItems.Any())
				MenuItemSelected = MenuItems.ElementAt (0);


			//Something has happened, we should inform user etc


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



//			Page splashPage = nav.Navigation.NavigationStack[0];
//
//			if (typeof (SplashPage) == splashPage.GetType()) 
//				return;
//			
//			nav.Navigation.RemovePage(splashPage);

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




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
	using HabitatApp.Repositories;


	public class NavigationTabbedPageViewModel : ViewModelBase
	{
		private readonly INavigationMenuService _navigationMenuService;
		private readonly IPageService _pageService;
		private readonly ISettingsRepository _settingsRepository;

		public NavigationTabbedPageViewModel (IPageService pageService, INavigationMenuService navigationMenuService, ISettingsRepository settingsRepository)
		{
			_pageService = pageService;
			_navigationMenuService = navigationMenuService;
			_settingsRepository = settingsRepository;
		}


		private ObservableCollection<Page> _tabbedPages = new ObservableCollection<Page> ();

		public ObservableCollection<Page> TabbedPages {
			get {
				return _tabbedPages;
			}
			set { SetProperty (ref _tabbedPages, value); }
		}

	
		private string _habitatLogo;

		public String HabitatLogo {
			get {
				return _habitatLogo;
			}
			set { SetProperty (ref _habitatLogo, value); }

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

		private async Task SetData(){

			IEnumerable<NavigationMenuItem> menuItems = await _navigationMenuService.GenerateMenuItems ();

			if (menuItems == null)
				return;

			List<Page> list = new List<Page> ();

			foreach (NavigationMenuItem menuItem in menuItems.Where(item => item.ShowInMenu)) {
				Page page = await _pageService.LoadPageByPageData (menuItem.PageContext);
				page.Icon = menuItem.IconSource;
				list.Add (page);
			}

			TabbedPages = list.ToObservableCollection ();

		} 





	}
}


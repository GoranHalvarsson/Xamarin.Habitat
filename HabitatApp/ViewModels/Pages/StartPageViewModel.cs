


namespace HabitatApp.ViewModels.Pages
{
	using System.Threading.Tasks;
	using System.Collections.ObjectModel;
	using Xamarin.Forms;
	using System.Linq;
	using System.Collections.Generic;
	using System.Windows.Input;
	using HabitatApp.Services;
	using HabitatApp.Models;
	using HabitatApp.Extensions;

	public class StartPageViewModel : ViewModelBase
	{

	
		private readonly IListItemService _listItemService;
		private INavigationService _navigationService;

		public StartPageViewModel (IListItemService listItemService, INavigationService navigationService)
		{
			_listItemService = listItemService;
			_navigationService = navigationService;

		}


		ListItem _currentCarouselItem;
		public ListItem CurrentCarouselItem {
			get {
				return _currentCarouselItem;
			}
			set {
				SetProperty (ref _currentCarouselItem, value);
			}
		}

		private int _selectedIndex;
		public int SelectedIndex {
			get {
				return _selectedIndex;
			}
			set {
				SetProperty (ref _selectedIndex, value);
			}
		}

		private int _scrollToIndex;
		public int ScrollToIndex {
			get {
				return _scrollToIndex;
			}
			set {
				SetProperty (ref _scrollToIndex, value);
			}
		}

	
		private ObservableCollection<ListItem> _carouselItems = new ObservableCollection<ListItem> ();

		public ObservableCollection<ListItem> CarouselItems {
			get {
				return _carouselItems;
			}
			set { SetProperty (ref _carouselItems, value); }
		}


		private Command _linkSelectedCommand;
		public ICommand LinkSelectedCommand
		{
			get
			{
				if (_linkSelectedCommand == null)
				{
					_linkSelectedCommand = new Command (async (parameter) =>  {
						string itemId = (string)parameter;

						if(string.IsNullOrWhiteSpace(itemId))
							return;

						await _navigationService.NavigateToPageByItemId(ConnectedToPage, itemId);

					});
				}

				return _linkSelectedCommand;
			}
		}


		/// <summary>
		/// Loads the async.
		/// </summary>
		/// <returns>The async.</returns>
		public async override Task  LoadAsync ()
		{

			base.SetBusy("Loading");

			PageData pageData = base.PageContext; 

			await SetData (pageData);

			base.ClearBusy();
		}

		private async Task SetData(PageData pageData){

			base.Title = pageData.ItemContext.FirstOrDefault ().GetValueFromField (Constants.Sitecore.Fields.PageContent.Title);

			IEnumerable<ListItem> carouselItems = await _listItemService.GenerateListItemsFromTeasers(pageData.DataSourceFromField);

			CarouselItems = carouselItems.ToObservableCollection ();

			CurrentCarouselItem = CarouselItems.First();

		}

	}

}


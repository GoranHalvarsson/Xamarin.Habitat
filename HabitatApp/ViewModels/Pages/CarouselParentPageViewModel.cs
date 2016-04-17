

namespace HabitatApp.ViewModels.Pages
{

	using System.Threading.Tasks;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	using Sitecore.MobileSDK.API.Items;
	using System.Windows.Input;
	using Xamarin.Forms;
	using HabitatApp.Services;
	using HabitatApp.Models;
	using HabitatApp.Extensions;
	using HabitatApp.Repositories;

	public class CarouselParentPageViewModel : ViewModelBase
	{
		private readonly IListItemService _listItemService;
		private readonly INavigationService _navigationService;
		private readonly ICachedMediaRepository _cachedMediaRepository;

		public CarouselParentPageViewModel (IListItemService listItemService, INavigationService navigationService, ICachedMediaRepository cachedMediaRepository)
		{
			_listItemService = listItemService;
			_navigationService = navigationService;
			_cachedMediaRepository = cachedMediaRepository;
		}

		private CachedMedia _media;

		public CachedMedia ContentMedia {
			get {
				return _media;
			}
			set { SetProperty (ref _media, value); }

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

		private ObservableCollection<ListItem> _carouselItems = new ObservableCollection<ListItem> ();

		public ObservableCollection<ListItem> CarouselItems {
			get {
				return _carouselItems;
			}
			set { 
				SetProperty (ref _carouselItems, value); 

				if (_carouselItems != null) {
					CurrentCarouselItem = _carouselItems.FirstOrDefault ();
				}
			}
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
		/// Entering page
		/// </summary>
		/// <returns>The async.</returns>
		public async override Task LoadAsync ()
		{

			SetBusy ("Loading");

			PageData pageData = base.PageContext;

			await SetData (pageData);

			ClearBusy ();

		}

		private async Task SetData(PageData pageData){
			
			ISitecoreItem item = pageData.ItemContext.FirstOrDefault ();

			base.Title = item.GetValueFromField (Constants.Sitecore.Fields.PageContent.Title);

			ContentHeader = item.GetValueFromField(Constants.Sitecore.Fields.PageContent.Title);
			ContentSummary = item.GetValueFromField(Constants.Sitecore.Fields.PageContent.Summary);
			ContentMedia =  await _cachedMediaRepository.GetCache(item.GetImageUrlFromMediaField (Constants.Sitecore.Fields.PageContent.Image));

			IEnumerable<ListItem> carouselItems = await _listItemService.GenerateListItemsFromChildren(pageData.DataSourceFromChildren);

			CarouselItems = carouselItems.ToObservableCollection ();

			CurrentCarouselItem = CarouselItems.First();

		}



	
	}
}


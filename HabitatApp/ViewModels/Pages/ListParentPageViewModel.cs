

namespace HabitatApp.ViewModels.Pages
{

	using System;
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

	public class ListParentPageViewModel : ViewModelBase
	{
		private readonly IListItemService _listItemService;
		private readonly INavigationService _navigationService;

		public ListParentPageViewModel (IListItemService listItemService, INavigationService navigationService)
		{
			_listItemService = listItemService;
			_navigationService = navigationService;

		}

		private string _contentImage = null;

		public string ContentImage {
			get {
				return _contentImage;
			}
			set { SetProperty (ref _contentImage, value); }
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

		private ObservableCollection<Tuple<ListItem,ListItem>> _listItems = new ObservableCollection<Tuple<ListItem,ListItem>> ();

		public ObservableCollection<Tuple<ListItem,ListItem>> ListItems {
			get {
				return _listItems;
			}
			set { 
				SetProperty (ref _listItems, value); 
			}
		}


		private ListItem _listItemSelected;

		public ListItem ListItemSelected {
			get {
				return _listItemSelected;
			}
			set {
				SetProperty (ref _listItemSelected, value);

				if (_listItemSelected != null) {

					//Async?
					_navigationService.NavigateToPageByItemId(ConnectedToPage, _listItemSelected.NavigationItem);

				}

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
			ContentImage = item.GetImageUrlFromMediaField (Constants.Sitecore.Fields.PageContent.Image);

			IEnumerable<ListItem> listItems = await _listItemService.GenerateListItemsFromChildren(pageData.DataSourceFromChildren);

			ListItems = listItems.ToList().AsPairsSafe ().ToObservableCollection ();

		

		}

		public async override Task UnLoadAsync ()
		{
			ClearBusy ();
		}

	
	}
}


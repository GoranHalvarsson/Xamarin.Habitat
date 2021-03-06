﻿

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
	using HabitatApp.Repositories;

	public class ListParentPageViewModel : ViewModelBase
	{
		private readonly IListItemService _listItemService;
		private readonly INavigationService _navigationService;
		private readonly ICachedMediaRepository _cachedMediaRepository;


		public ListParentPageViewModel (IListItemService listItemService, INavigationService navigationService, ICachedMediaRepository cachedMediaRepository)
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

		private ObservableCollection<Tuple<ListItem,ListItem>> _listItems = new ObservableCollection<Tuple<ListItem,ListItem>> ();

		public ObservableCollection<Tuple<ListItem,ListItem>> ListItems {
			get {
				return _listItems;
			}
			set { 
				SetProperty (ref _listItems, value); 
			}
		}

		private ObservableCollection<ListItem> _list = new ObservableCollection<ListItem> ();

		public ObservableCollection<ListItem> ListOfItems {
			get {
				return _list;
			}
			set { 
				SetProperty (ref _list, value); 
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



			//UpdateMediaData ();

		}

		private async Task SetData(PageData pageData){
			
			ISitecoreItem item = pageData.ItemContext.FirstOrDefault ();

			base.Title = item.GetValueFromField (Constants.Sitecore.Fields.PageContent.Title);

			ContentHeader = item.GetValueFromField(Constants.Sitecore.Fields.PageContent.Title);
			ContentSummary = item.GetValueFromField(Constants.Sitecore.Fields.PageContent.Summary);
			ContentMedia =  await _cachedMediaRepository.GetCache(item.GetImageUrlFromMediaField (Constants.Sitecore.Fields.PageContent.Image));

			IEnumerable<ListItem> listItems = await _listItemService.GenerateListItemsFromChildren(pageData.DataSourceFromChildren);

			ListItems = listItems.ToList().AsPairsSafe ().ToObservableCollection ();


		}

		private async Task UpdateMediaData(){

			foreach (Tuple<ListItem, ListItem> tupleItem in ListItems) {

				await Task.Delay(2000);

				tupleItem.Item1.Media = await _cachedMediaRepository.GetCache (tupleItem.Item1.SitecoreItem.GetImageUrlFromMediaField (Constants.Sitecore.Fields.PageContent.Image));
				tupleItem.Item2.Media = await _cachedMediaRepository.GetCache (tupleItem.Item2.SitecoreItem.GetImageUrlFromMediaField (Constants.Sitecore.Fields.PageContent.Image));
			}

		}

	
	}
}


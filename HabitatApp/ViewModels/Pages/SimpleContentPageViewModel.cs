

namespace HabitatApp.ViewModels.Pages
{
	
	using System.Threading.Tasks;
	using Sitecore.MobileSDK.API.Items;
	using System.Linq;
	using HabitatApp.Models;
	using HabitatApp.Extensions;
	using HabitatApp.Repositories;

	public class SimpleContentPageViewModel: ViewModelBase
	{
		private readonly ICachedMediaRepository _cachedMediaRepository;

		public SimpleContentPageViewModel (ICachedMediaRepository cachedMediaRepository)
		{
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

		private string _contentBody = string.Empty;

		public string ContentBody {
			get {
				return _contentBody;
			}
			set { 
				SetProperty (ref _contentBody, value); 
			}
		}


		/// <summary>
		/// Entering page
		/// </summary>
		/// <returns>The async.</returns>
		public async override Task LoadAsync ()
		{

			SetBusy ("Loading");

			await SetData (base.PageContext);

			ClearBusy ();

		}


		private async Task SetData(PageData pageData){

			ISitecoreItem item = pageData.ItemContext.FirstOrDefault();

			base.Title = pageData.NavigationTitle;

			ContentBody = item.GetValueFromField(Constants.Sitecore.Fields.PageContent.Body);
			ContentHeader = item.GetValueFromField(Constants.Sitecore.Fields.PageContent.Title);
			ContentSummary = item.GetValueFromField(Constants.Sitecore.Fields.PageContent.Summary);

			ContentMedia =  await _cachedMediaRepository.GetCache(item.GetImageUrlFromMediaField (Constants.Sitecore.Fields.PageContent.Image));

		} 


	}
}




namespace HabitatApp.ViewModels.Pages
{
	
	using System.Threading.Tasks;
	using Sitecore.MobileSDK.API.Items;
	using System.Linq;
	using HabitatApp.Models;
	using HabitatApp.Extensions;

	public class SimpleContentPageViewModel: ViewModelBase
	{
		public SimpleContentPageViewModel ()
		{
			
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

			SetData (base.PageContext);

			ClearBusy ();

		}

		public async override Task UnLoadAsync ()
		{
			ClearBusy ();
		}

		private void SetData(PageData pageData){

			ISitecoreItem item = pageData.ItemContext.FirstOrDefault();

			base.Title = pageData.NavigationTitle;

			ContentBody = item.GetValueFromField(Constants.Sitecore.Fields.PageContent.Body);
			ContentHeader = item.GetValueFromField(Constants.Sitecore.Fields.PageContent.Title);
			ContentSummary = item.GetValueFromField(Constants.Sitecore.Fields.PageContent.Summary);
			ContentImage = item.GetImageUrlFromMediaField (Constants.Sitecore.Fields.PageContent.Image);

		} 


	}
}


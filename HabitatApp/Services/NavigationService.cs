namespace HabitatApp.Services
{
	using Xamarin.Forms;
	using System.Threading.Tasks;
	using HabitatApp.Models;
	using HabitatApp.ViewModels;

	public class NavigationService : INavigationService
	{

		private readonly IPageService _pageService;

		public NavigationService(IPageService pageService)
		{
			_pageService = pageService;
		}

		public async Task NavigateToPageByItemId (Page navigateFromPage, string itemId)
		{
			Page page = await _pageService.LoadPageByItemId(itemId);
			await NavigateAndLoadBindingContext (navigateFromPage, page);
		}

		public async Task NavigateToPageByPageData (Page navigateFromPage, PageData pageData)
		{
			Page page = await _pageService.LoadPageByPageData(pageData);
			await NavigateAndLoadBindingContext (navigateFromPage, page);
		}


		private async Task NavigateAndLoadBindingContext(Page navigateFromPage, Page navigateToPage){

			IViewModel viewModel = (IViewModel)navigateToPage.BindingContext;


			viewModel.ConnectedToPage = navigateFromPage;

			//We need to load it all before page appears
			//Page.Appering() gives an unfortunate delay
			viewModel.LoadAsync ();

			//navigateToPage.Appearing += async (object sender, EventArgs e) => await viewModel.LoadAsync ();

			//navigateToPage.Disappearing += async (object sender, EventArgs e) => await viewModel.UnLoadAsync ();


			await navigateFromPage.Navigation.PushAsync (navigateToPage);

		}



	}
}


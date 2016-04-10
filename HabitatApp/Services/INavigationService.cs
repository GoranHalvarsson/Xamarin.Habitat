


namespace HabitatApp.Services
{
	using System.Threading.Tasks;
	using Xamarin.Forms;
	using HabitatApp.Models;

	public interface INavigationService
	{


		Task NavigateToPageByItemId (Page navigateFromPage, string itemId);

		Task NavigateToPageByPageData (Page navigateFromPage, PageData pageData);

	}
}


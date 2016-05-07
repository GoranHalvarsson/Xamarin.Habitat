
namespace HabitatApp.Services
{

	using System;
	using Xamarin.Forms;
	using HabitatApp.Models;
	using System.Threading.Tasks;


	public interface IPageService
	{
		Task<Page> LoadPageByPageData (PageData pageData);

		Task<Page> LoadPageByItemId(string id);
	}
}


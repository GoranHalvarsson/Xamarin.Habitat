


namespace HabitatApp.Services
{

	using HabitatApp.Models;
	using System.Threading.Tasks;
	using System.Collections.Generic;


	public interface INavigationMenuService
	{
		Task<IEnumerable<NavigationMenuItem>> GenerateMenuItems();
	}
}


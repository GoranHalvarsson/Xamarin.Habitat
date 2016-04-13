

namespace HabitatApp.ViewModels
{
	using HabitatApp.Models;
	using System.Threading.Tasks;
	using Xamarin.Forms;


	public interface IViewModel 
	{
		string Title { get; set; }

		PageData PageContext { get; set; }

		Page ConnectedToPage{ get; set; }

		Task LoadAsync ();

		Task UnLoadAsync ();


	}
}


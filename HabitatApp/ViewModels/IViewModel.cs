

namespace HabitatApp.ViewModels
{
	using System;
	using System.ComponentModel;
	using HabitatApp.Models;
	using System.Threading.Tasks;
	using Xamarin.Forms;


	public interface IViewModel : INotifyPropertyChanged
	{
		string Title { get; set; }

		void SetState<T>(Action<T> action) where T : class, IViewModel;

		PageData PageContext { get; set; }

		Page ConnectedToPage {
			get; 
			set; 
		}

		Task LoadAsync ();

		Task UnLoadAsync ();


	}
}


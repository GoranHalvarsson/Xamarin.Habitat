﻿

namespace HabitatApp.Views.Pages
{
	using Xamarin.Forms;
	using Xamarin.Forms.Xaml;
	using HabitatApp.ViewModels.Pages;
	using Autofac;

	[XamlCompilation (XamlCompilationOptions.Compile)]
	public partial class NavigationMasterPage : MasterDetailPage
	{

		private NavigationMasterViewModel _navigationMasterViewModel;

		public NavigationMasterPage () : this(HabitatApp.App.AppInstance.Container.Resolve<NavigationMasterViewModel>())
		{

		}

		public NavigationMasterPage (NavigationMasterViewModel navigationMasterViewModel)
		{
			InitializeComponent ();

			_navigationMasterViewModel = navigationMasterViewModel;

			_navigationMasterViewModel.ConnectedToPage = this;

			BindingContext = _navigationMasterViewModel;
			//This is for Android, we dont want to have the row selected in the menu
			listView.ItemSelected += (s, e) => {
				listView.SelectedItem = null; 
			};


		}


		protected override async void OnAppearing ()
		{
			await _navigationMasterViewModel.LoadAsync();

			base.OnAppearing ();
		}



	}
}


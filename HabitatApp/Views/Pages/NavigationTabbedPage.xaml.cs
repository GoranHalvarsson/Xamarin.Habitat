

namespace HabitatApp.Views.Pages
{

	using System;
	using System.Collections.Generic;

	using Xamarin.Forms;
	using Xamarin.Forms.Xaml;
	using HabitatApp.ViewModels.Pages;
	using Autofac;

	[XamlCompilation (XamlCompilationOptions.Compile)]
	public partial class NavigationTabbedPage : BindableTabbedPage
	{

		private NavigationTabbedPageViewModel _navigationTabbedPageViewModel;

		public NavigationTabbedPage () : this(HabitatApp.App.AppInstance.Container.Resolve<NavigationTabbedPageViewModel>())
		{
		}

		public NavigationTabbedPage (NavigationTabbedPageViewModel navigationTabbedPageViewModel)
		{
			InitializeComponent ();

			_navigationTabbedPageViewModel = navigationTabbedPageViewModel;

			_navigationTabbedPageViewModel.ConnectedToPage = this;

		}

		protected override async void OnAppearing ()
		{
			await _navigationTabbedPageViewModel.LoadAsync();

			BindingContext = _navigationTabbedPageViewModel;

			base.OnAppearing ();


		}

	}
}




namespace HabitatApp.Views.Pages
{

	using Autofac;
	using HabitatApp.ViewModels.Pages;

	using Xamarin.Forms;

	public partial class TabbedNavigationPage : TabbedPage
	{
		private NavigationTabbedPageViewModel _navigationTabbedPageViewModel;

		public TabbedNavigationPage () : this(HabitatApp.App.AppInstance.Container.Resolve<NavigationTabbedPageViewModel>())
		{
		}

		public TabbedNavigationPage (NavigationTabbedPageViewModel navigationTabbedPageViewModel)
		{
			InitializeComponent ();

			_navigationTabbedPageViewModel = navigationTabbedPageViewModel;

			_navigationTabbedPageViewModel.ConnectedToPage = this;

			BindingContext = _navigationTabbedPageViewModel;
		}

		protected override async void OnAppearing ()
		{
			await _navigationTabbedPageViewModel.LoadAsync();

			base.OnAppearing ();
		}
	}
}


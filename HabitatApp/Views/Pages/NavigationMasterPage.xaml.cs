using Xamarin.Forms.Xaml;

namespace HabitatApp.Views.Pages
{


	using Xamarin.Forms;
	using HabitatApp.ViewModels.Pages;
	using Autofac;

	[XamlCompilation (XamlCompilationOptions.Compile)]
	public partial class NavigationMasterPage : MasterDetailPage
	{

		private NavigationMasterViewModel _navigationMasterViewModel;

		public NavigationMasterPage () : this(HabitatApp.App.Instance.Container.Resolve<NavigationMasterViewModel>())
		{

		}

		public NavigationMasterPage (NavigationMasterViewModel navigationMasterViewModel)
		{
			InitializeComponent ();

			_navigationMasterViewModel = navigationMasterViewModel;

			BindingContext = _navigationMasterViewModel;

			_navigationMasterViewModel.ConnectedToPage = this;

		}


		protected override async void OnAppearing ()
		{
			await _navigationMasterViewModel.LoadAsync();

			base.OnAppearing ();
		}



	}
}


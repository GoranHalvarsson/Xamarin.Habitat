

namespace HabitatApp.Views.Pages
{

	using Xamarin.Forms;
	using Autofac;
	using HabitatApp.ViewModels.Pages;


	public partial class SplashPage : ContentPage
	{
		private readonly SplashPageViewModel _splashPageViewModel;

		public SplashPage() : this(App.AppInstance.Container.Resolve<SplashPageViewModel>())
		{

		}

		public SplashPage (SplashPageViewModel splashPageViewModel)
		{
			InitializeComponent ();

			_splashPageViewModel = splashPageViewModel;

			_splashPageViewModel.ConnectedToPage = this;

			BindingContext = _splashPageViewModel;

		}

		protected override async void OnAppearing ()
		{
			await _splashPageViewModel.LoadAsync();
		
			base.OnAppearing ();


		}
	}
}


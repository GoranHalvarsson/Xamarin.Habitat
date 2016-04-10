namespace HabitatApp.Views.Pages
{
	using Xamarin.Forms.Xaml;
	using Xamarin.Forms;
	using HabitatApp.ViewModels.Pages;
	using Autofac;

	[XamlCompilation (XamlCompilationOptions.Compile)]
	public partial class StartPage : ContentPage
	{
		private readonly StartPageViewModel _startPageViewModel;

		public StartPage() : this(App.Instance.Container.Resolve<StartPageViewModel>())
		{

		}

		public StartPage (StartPageViewModel startPageViewModel)
		{
			InitializeComponent ();

			_startPageViewModel = startPageViewModel;

			BindingContext = _startPageViewModel;

		}
	}
}


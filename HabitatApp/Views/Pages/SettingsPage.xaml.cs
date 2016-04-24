

namespace HabitatApp.Views.Pages
{
	using Xamarin.Forms;
	using Autofac;
	using HabitatApp.ViewModels.Pages;
	using Xamarin.Forms.Xaml;

	[XamlCompilation (XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		private readonly SettingsPageViewModel _settingsPageViewModel;

		public SettingsPage() : this(App.AppInstance.Container.Resolve<SettingsPageViewModel>())
		{

		}

		public SettingsPage (SettingsPageViewModel settingsPageViewModel)
		{
			InitializeComponent ();

			_settingsPageViewModel = settingsPageViewModel;

			_settingsPageViewModel.ConnectedToPage = this;

			BindingContext = _settingsPageViewModel;

		}

		protected async override void OnDisappearing ()
		{

			await _settingsPageViewModel.UnLoadAsync();

			base.OnDisappearing ();
		}

	}
}


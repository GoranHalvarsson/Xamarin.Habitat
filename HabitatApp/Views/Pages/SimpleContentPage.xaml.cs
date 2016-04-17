

namespace HabitatApp.Views.Pages
{
	using Xamarin.Forms;
	using Autofac;
	using HabitatApp.ViewModels.Pages;
	using Xamarin.Forms.Xaml;

	[XamlCompilation (XamlCompilationOptions.Compile)]
	public partial class SimpleContentPage : ContentPage
	{
		private readonly SimpleContentPageViewModel _simpleContentPageViewModel;

		public SimpleContentPage() : this(App.AppInstance.Container.Resolve<SimpleContentPageViewModel>())
		{

		}

		public SimpleContentPage (SimpleContentPageViewModel simpleContentPageViewModel)
		{
			InitializeComponent ();

			_simpleContentPageViewModel = simpleContentPageViewModel;

			_simpleContentPageViewModel.ConnectedToPage = this;

			BindingContext = _simpleContentPageViewModel;

		}


	}
}


namespace HabitatApp.Views.Pages
{
	using HabitatApp.ViewModels.Pages;
	using Xamarin.Forms;
	using Xamarin.Forms.Xaml;
	using Autofac;


	[XamlCompilation (XamlCompilationOptions.Compile)]
	public partial class CarouselParentPage : ContentPage
	{
		private readonly CarouselParentPageViewModel _carouselParentPageViewModel;

		public CarouselParentPage() : this(App.Instance.Container.Resolve<CarouselParentPageViewModel>())
		{

		}

		public CarouselParentPage (CarouselParentPageViewModel carouselParentPageViewModel)
		{
			InitializeComponent ();

			_carouselParentPageViewModel = carouselParentPageViewModel;

			BindingContext = _carouselParentPageViewModel;

		}
	}
}


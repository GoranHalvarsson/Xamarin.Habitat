

namespace HabitatApp
{

	using Xamarin.Forms;
	using Autofac;

	public partial class App : Application
	{

		public IContainer Container;

		public static App Instance; 

		public App ()
		{
			Instance = this;

			InitializeComponent();

			Bootstrapper.Run();

		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}


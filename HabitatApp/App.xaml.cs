using Plugin.Connectivity;
using System.Threading.Tasks;
using System;

namespace HabitatApp
{

	using Xamarin.Forms;
	using Autofac;

	public partial class App : Application
	{

		public IContainer Container;

		public static App AppInstance; 

		public App ()
		{
			AppInstance = this;

			InitializeComponent();

			Bootstrapper.Run();

		}

		public async Task ExecuteIfConnected(Func<Task> actionToExecuteIfConnected)
		{
			if (CrossConnectivity.Current.IsConnected)
			{
				await actionToExecuteIfConnected();
			}
			else
			{
				await ShowNetworkConnectionAlert();
			}
		}



		private async Task ShowNetworkConnectionAlert()
		{
			await AppInstance.MainPage.DisplayAlert(
				"Network Issues", 
				"Some issues with network", 
				"Close");
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


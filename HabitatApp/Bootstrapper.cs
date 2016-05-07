

namespace HabitatApp
{

	using Autofac;
	using HabitatApp.Views.Pages;
	using Xamarin.Forms;

	public static class Bootstrapper
	{
		public static void Run()
		{

			ContainerBuilder builder = new ContainerBuilder();
			builder.RegisterModule<HabitatAppModule>();
			HabitatApp.App.AppInstance.Container = builder.Build();

			HabitatApp.App.AppInstance.MainPage = HabitatApp.App.AppInstance.Container.Resolve<SplashPage> ();

//			using (ILifetimeScope lifetimeScope = HabitatApp.App.AppInstance.Container.BeginLifetimeScope())
//			{
//				HabitatApp.App.AppInstance.MainPage = lifetimeScope.Resolve<SplashPage> ()
//
////				if(Device.OS == TargetPlatform.iOS)
////					HabitatApp.App.AppInstance.MainPage = lifetimeScope.Resolve<NavigationTabbedPage> ();
////				else
////					HabitatApp.App.AppInstance.MainPage = lifetimeScope.Resolve<NavigationMasterPage> ();
//
//			}
		}
	}
}


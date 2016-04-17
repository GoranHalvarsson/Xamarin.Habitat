

namespace HabitatApp
{

	using Autofac;
	using HabitatApp.Views.Pages;

	public static class Bootstrapper
	{
		public static void Run()
		{

			ContainerBuilder builder = new ContainerBuilder();
			builder.RegisterModule<HabitatAppModule>();
			HabitatApp.App.AppInstance.Container = builder.Build();

			using (ILifetimeScope lifetimeScope = HabitatApp.App.AppInstance.Container.BeginLifetimeScope())
			{
				HabitatApp.App.AppInstance.MainPage = lifetimeScope.Resolve<NavigationMasterPage> ();
			}
		}
	}
}


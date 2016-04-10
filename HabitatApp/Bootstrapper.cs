

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
			HabitatApp.App.Instance.Container = builder.Build();

			using (ILifetimeScope lifetimeScope = HabitatApp.App.Instance.Container.BeginLifetimeScope())
			{
				HabitatApp.App.Instance.MainPage = lifetimeScope.Resolve<NavigationMasterPage> ();
			}
		}
	}
}


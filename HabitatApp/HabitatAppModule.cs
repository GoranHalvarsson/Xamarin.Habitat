
namespace HabitatApp
{

	using Autofac;
	using HabitatApp.Repositories;
	using HabitatApp.Services;
	using HabitatApp.ViewModels.Pages;
	using HabitatApp.Views.Pages;

	public class HabitatAppModule: Module
	{
		protected override void Load (ContainerBuilder builder)
		{
			base.Load (builder);

			builder.RegisterType<SQLiteConnectionService> ()
				.As<ISQLiteConnectionService> ()
				.SingleInstance ();

			builder.RegisterType<SettingsRepository> ()
				.As<ISettingsRepository> ()
				.SingleInstance ();

			builder.RegisterType<LoggingService> ()
				.As<ILoggingService> ()
				.SingleInstance ();

		
			builder.RegisterType<SitecoreService> ()
				.As<ISitecoreService> ()
				.SingleInstance ();

			builder.RegisterType<NavigationMenuService> ()
				.As<INavigationMenuService> ()
				.SingleInstance ();

			builder.RegisterType<ListItemService> ()
				.As<IListItemService> ()
				.SingleInstance ();

			builder.RegisterType<NavigationService> ()
				.As<INavigationService> ()
				.SingleInstance ();



			builder.RegisterType<NavigationMasterViewModel> ();
			builder.RegisterType<NavigationMasterPage> ();

			builder.RegisterType<StartPageViewModel> ();
			builder.RegisterType<StartPage> ();

			builder.RegisterType<SimpleContentPageViewModel> ();
			builder.RegisterType<SimpleContentPage> ();

			builder.RegisterType<CarouselParentPageViewModel> ();
			builder.RegisterType<CarouselParentPage> ();

			builder.RegisterType<ListParentPageViewModel> ();
			builder.RegisterType<ListParentPage> ();

		
				
	

		}

	}
}


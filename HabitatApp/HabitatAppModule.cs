﻿
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

			builder.RegisterType<RestService> ()
				.As<IRestService> ()
				.SingleInstance ();

			builder.RegisterType<CacheValidatorService> ()
				.As<ICacheValidatorService> ()
				.SingleInstance ();

			builder.RegisterType<CachedMediaRepository> ()
				.As<ICachedMediaRepository> ()
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

			builder.RegisterType<PageService> ()
				.As<IPageService> ()
				.SingleInstance ();

			builder.RegisterType<NavigationService> ()
				.As<INavigationService> ()
				.SingleInstance ();

			builder.RegisterType<SplashPageViewModel> ();
			builder.RegisterType<SplashPage> ();


			builder.RegisterType<NavigationTabbedPageViewModel> ();
			builder.RegisterType<NavigationTabbedPage> ();

			builder.RegisterType<NavigationMasterViewModel> ();
			builder.RegisterType<NavigationMasterPage> ();

			builder.RegisterType<SettingsPageViewModel> ();
			builder.RegisterType<SettingsPage> ();

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


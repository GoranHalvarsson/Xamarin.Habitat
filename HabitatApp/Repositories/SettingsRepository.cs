namespace HabitatApp.Repositories
{

	using System;
	using HabitatApp.Services;
	using SQLite.Net.Async;
	using System.Threading;
	using System.Threading.Tasks;
	using HabitatApp.Models;

	public class SettingsRepository : ISettingsRepository
	{
		private ILoggingService _loggingService;
		private readonly SQLiteAsyncConnection _asyncConnection;
		private readonly SemaphoreSlim _repositoryLock = new SemaphoreSlim (1);

		public SettingsRepository (ILoggingService loggingService, ISQLiteConnectionService sqLiteConnectionService)
		{
			_loggingService = loggingService;

			_asyncConnection = sqLiteConnectionService.AsyncConnection;

			_asyncConnection.CreateTableAsync<Settings> ();
		}


		private async Task Create ()
		{

			Settings settings = new Settings () {
				RestBaseUrl =  "http://myhabitat.dev",
				SitecoreNavigationRootId = "{061E42A3-7CD2-48BC-A611-963DF88AFFE0}",
				SitecoreNavigationRootPath = "/sitecore/content/Habitat/Mobile App",
				SitecoreUserName = "sitecore\\admin",
				SitecorePassword = "b",
				SitecoreShellSite = "/sitecore/shell",
				SitecoreDefaultDatabase = "master",
				SitecoreDefaultLanguage = "en",
				SitecoreMediaLibraryRoot = "/sitecore/media library",
				SitecoreMediaPrefix = "~/media/",
				SitecoreDefaultMediaResourceExtension = "ashx"

			};

			await _repositoryLock.WaitAsync ();

			try {
				await _asyncConnection.InsertAsync(settings);
			} catch (Exception ex) {
				_loggingService.Log ("Error in Insert,  SettingsRepository . Error: {0}", ex.Message); 
				throw ex;
			} finally { 
				_repositoryLock.Release ();
			}

		}		


		public async Task<Settings> Get ()
		{
			Settings settings = null;
			try {
				settings = await _asyncConnection.GetAsync<Settings> (s => s.RestBaseUrl != null);
			} catch (System.Exception ex) {
				_loggingService.Log ("Error in reading object from DB,  SettingsRepository . Error: {0}", ex.Message); 
			}

			return settings;
		}


		public async Task<Models.Settings> GetWithFallback ()
		{
			Settings settings = await Get ();

			if (settings != null)
				return settings;

			await Create ();

			return await Get ();
		}




		public async Task<Models.Settings> Update (Settings settings)
		{
			await _repositoryLock.WaitAsync ();

			try {
				await _asyncConnection.UpdateAsync (settings);
			} catch (Exception ex) {
				_loggingService.Log ("Error in Update,  SettingsRepository . Error: {0}", ex.Message); 
			} finally { 
				_repositoryLock.Release ();
			}

			return await Get ();
		}

		public async Task<bool> DeleteAll (){

			Int32 result = 0;

			await _repositoryLock.WaitAsync ();

			try {

				result = await _asyncConnection.DeleteAllAsync<Settings> ();

			} finally { 
				_repositoryLock.Release ();
			}

			return result > 0;


		}

	}
}


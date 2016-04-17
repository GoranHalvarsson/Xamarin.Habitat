namespace HabitatApp.Repositories
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;
	using SQLite.Net.Async;
	using HabitatApp.Services;
	using HabitatApp.Models;
	using HabitatApp.Exceptions.Rest;

	public class CachedMediaRepository : ICachedMediaRepository
	{
		private readonly ILoggingService _loggingService;
		private readonly ICacheValidatorService _cacheValidatorService;
		private readonly SQLiteAsyncConnection _asyncConnection;
		private readonly IRestService _restService;
		private readonly ISitecoreService _sitecoreService;
		private readonly ISettingsRepository _settingsRepository;
		private readonly SemaphoreSlim _repositoryLock = new SemaphoreSlim (1);
		private readonly Task<Settings> _settings;

		private readonly bool _useHostInUrl = false;  
	
		public CachedMediaRepository (
			ILoggingService loggingService, 
			IRestService restService,
			ISettingsRepository settingsRepository,
			ISitecoreService sitecoreService,
			ICacheValidatorService cacheValidatorService,
			ISQLiteConnectionService sqLiteConnectionService)
		{
			_loggingService = loggingService;
			_restService = restService;
			_settingsRepository = settingsRepository;
			_sitecoreService = sitecoreService;
			_cacheValidatorService = cacheValidatorService;
	
			_settings = _settingsRepository.GetWithFallback ();
			_useHostInUrl = true;
			_asyncConnection = sqLiteConnectionService.AsyncConnection;
			_asyncConnection.CreateTableAsync<CachedMedia> ();
		}


		private async Task<string> GeneratRestUrl(string url) 
		{
			if (!_useHostInUrl)
				return url;

			Settings settings = await _settings;

			return String.Format("{0}/{1}", settings.RestBaseUrl, url);
		
		}


		/// <summary>
		/// Create media by id, url and mediaType.
		/// </summary>
		/// <param name="id">Media identifier.</param>
		/// <param name="url">URL.</param>
		/// <param name="mediaType">Media type.</param>
		public  CachedMedia Create (Guid id, string url, string mediaType = "Image")
		{
			return new CachedMedia () {
				Id = id,
				Url = url,
				MediaType = mediaType
			};

		}

		/// <summary>
		/// Get media by id.
		/// </summary>
		/// <param name="id">Media identifier.</param>
		public async Task<CachedMedia> Get (Guid id)
		{
			CachedMedia cachedMedia = null;

			try {
			
				cachedMedia = await this._asyncConnection.GetAsync<CachedMedia> (cached => cached.Id == id);
		
			} catch (System.Exception ex) {
				_loggingService.Log ("Error in reading media from DB,  id {0} . Error: {1}", id, ex.Message);
				throw ex;	
			}

			return cachedMedia;

		}

		/// <summary>
		/// Get media by media url.
		/// </summary>
		/// <param name="url">Media URL.</param>
		public async Task<CachedMedia> Get (string url)
		{


			CachedMedia cachedMedia = null;

			try 
			{

				cachedMedia = await this._asyncConnection.FindAsync<CachedMedia> (url);

			} catch (System.Exception ex) {
				_loggingService.Log ("Error in reading image from DB,  url {0} . Error: {1}", url, ex.Message); 
				throw ex;	
			} 

			return cachedMedia;

		}

		/// <summary>
		/// Delete media by id.
		/// </summary>
		/// <param name="id">Identifier.</param>
		public async Task<bool> Delete (Guid id)
		{
			CachedMedia current = await Get (id);

			if (current == null)
				return false;

			return await Delete (current.Url);

		}

	
		/// <summary>
		/// Delete media by url.
		/// </summary>
		/// <param name="url">URL.</param>
		public async Task<bool> Delete (string url)
		{
			Int32 result = 0;

			await _repositoryLock.WaitAsync ();

			try {

				result = await _asyncConnection.DeleteAsync<CachedMedia> (url);

				return result > 0;

			} catch (System.Exception ex) {
				_loggingService.Log ("Error in reading image from DB,  url {0} . Error: {1}", url, ex.Message); 
				throw ex;	
			} finally { 
				_repositoryLock.Release ();
			}



		}

		/// <summary>
		/// Gets the cache.
		/// </summary>
		/// <returns>The cache.</returns>
		/// <param name="id">Identifier.</param>
		/// <param name="url">URL.</param>
		/// <param name="mediaType">Media type.</param>
		/// <param name="overrideCache">If set to <c>true</c> override cache.</param>
		public async Task<CachedMedia> GetCache (string url, string mediaType = "Image", bool overrideCache = false)
		{

			//Get cached data from current request
			CachedMedia cachedMedia = await this.Get (url); 

		
			//Valid cache
			if (await _cacheValidatorService.IsCacheValid (cachedMedia, overrideCache))
				return cachedMedia;

			if (cachedMedia == null)
				cachedMedia = Create (Guid.NewGuid(), url, mediaType);

			try {

				//cachedMedia.MediaData = await _restService.GetAsync<Byte[]>(await GeneratRestUrl(cachedMedia.Url), CancellationToken.None);

				cachedMedia.MediaData = await _sitecoreService.GetMediaByUrl(cachedMedia.Url);

				await this.Save (cachedMedia);


			} catch (MethodFailedException ex) {
				_loggingService.Log ("KnownError in GetCache,  Url {0} . Error: {1}", url, ex.Message);
			} catch (System.Exception ex) {
				_loggingService.Log ("Error in GetCache,  Url {0} . Error: {1}", url, ex.Message);
			} 

			return cachedMedia;

		}

		/// <summary>
		/// Save CachedMedia
		/// </summary>
		/// <param name="newMediaRequest">New media request.</param>
		private async Task Save (CachedMedia newMediaRequest)
		{
			newMediaRequest.UpdatedAt = DateTime.Now;

			CachedMedia currentImageRequest = await Get (newMediaRequest.Url);

		
			try {

				if(currentImageRequest == null)
					await _asyncConnection.InsertAsync (newMediaRequest);
				else
					await _asyncConnection.UpdateAsync (newMediaRequest);

			} catch (Exception ex) {
				this._loggingService.Log ("Error in Save,  Url {0} . Error: {1}", newMediaRequest.Url, ex.Message);
				throw ex;
			} 

		}

		/// <summary>
		/// Drops all.
		/// </summary>
		/// <returns>The all.</returns>
		public async Task<bool> DropAll (){

			Int32 result = 0;

			await _repositoryLock.WaitAsync ();

			try {

				result = await this._asyncConnection.DropTableAsync<CachedMedia> ();
			} finally { 
				_repositoryLock.Release ();
			}

			return result > 0;


		}



	}
}



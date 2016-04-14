namespace HabitatApp.Services
{
	
	using HabitatApp.Repositories;
	using HabitatApp.Models;
	using Plugin.Connectivity;
	using System;
	using System.Threading.Tasks;

	public class CacheValidatorService : ICacheValidatorService
	{

		private readonly ISettingsRepository _settingsRepository;

		public CacheValidatorService (ISettingsRepository settingsRepository)
		{
			_settingsRepository = settingsRepository;
		}

		public async Task<bool> IsCacheValid<T>(T cachedObject, bool overrideCache = false) where T : CachedData
		{

			if (overrideCache)
				return false;

			Settings settings =  await _settingsRepository.Get ();

			if (!CrossConnectivity.Current.IsConnected)
				return true;

			if(cachedObject == null || !cachedObject.UpdatedAt.HasValue)
				return false;

			return DateTime.Now < cachedObject.UpdatedAt.Value.AddHours(settings.CachingInHours);
		}
	}
}


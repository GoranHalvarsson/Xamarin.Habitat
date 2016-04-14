using HabitatApp.Models;

namespace HabitatApp.Services
{
	using System;
	using System.Threading.Tasks;

	public interface ICacheValidatorService
	{
		Task<bool> IsCacheValid<T>(T cachedObject, bool overrideCache = false) where T: CachedData;
	}
}


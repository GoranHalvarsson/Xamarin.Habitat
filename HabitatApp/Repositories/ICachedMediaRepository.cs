using System;
using HabitatApp.Models;
using System.Threading.Tasks;

namespace HabitatApp.Repositories
{
	public interface ICachedMediaRepository
	{

		CachedMedia Create (Guid id, string url, string mediaType = "Image");

		Task<CachedMedia> GetCache (string url, string mediaType = "Image", bool overrideCache = false);

		Task<CachedMedia> Get (string url);

		Task<CachedMedia> Get (Guid id);

		Task<bool> Delete (Guid id);

		Task<bool> Delete (string url);

		Task<bool> DropAll ();

	}
}


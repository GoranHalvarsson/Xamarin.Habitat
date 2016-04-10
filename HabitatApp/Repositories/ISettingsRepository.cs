namespace HabitatApp.Repositories
{
	using System;
	using System.Threading.Tasks;

	public interface ISettingsRepository
	{
		Task<Models.Settings> Get ();

		Task<Models.Settings> GetWithFallback ();

		Task<Models.Settings> Update(Models.Settings settings);

		Task<bool> DeleteAll();
	}
}


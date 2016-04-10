namespace HabitatApp.Services
{

	using SQLite.Net.Async;


	public interface ISQLiteConnectionService
	{
		SQLiteAsyncConnection AsyncConnection {
			get;
		}
	}

}


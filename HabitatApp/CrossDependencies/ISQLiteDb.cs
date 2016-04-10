namespace HabitatApp.CrossDependencies
{

	using SQLite.Net;
	using SQLite.Net.Async;

	public interface ISQLiteDb
	{
		SQLiteConnection GetConnection ();

		SQLiteAsyncConnection GetAsyncConnection ();
	}
}


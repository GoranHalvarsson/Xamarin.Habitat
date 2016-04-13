


namespace HabitatApp.Services
{

	using SQLite.Net.Async;
	using Xamarin.Forms;
	using HabitatApp.Services;
	using HabitatApp.CrossDependencies;

	public class SQLiteConnectionService : ISQLiteConnectionService
	{
		
		private readonly SQLiteAsyncConnection _asyncConnection;

		public SQLiteConnectionService ()
		{
			_asyncConnection = DependencyService.Get<ISQLiteDb>().GetAsyncConnection ();
		}



		public SQLiteAsyncConnection AsyncConnection {

			get{ 
				return _asyncConnection;
			}
		}


	}
}


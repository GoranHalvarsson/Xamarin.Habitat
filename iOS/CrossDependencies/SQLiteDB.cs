using HabitatApp.iOS.CrossDependencies;

[assembly: Xamarin.Forms.Dependency(typeof(SQLiteDb))]
namespace HabitatApp.iOS.CrossDependencies
{
	using System;
	using HabitatApp.CrossDependencies;
	using SQLite.Net;
	using SQLite.Net.Platform.XamarinIOS;
	using System.IO;
	using SQLite.Net.Async;

	public class SQLiteDb : ISQLiteDb
	{

		private const string _fileName = "HabitatLocal.db3";

		public SQLiteDb ()
		{
		}

		public SQLiteConnection GetConnection ()
		{
			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			string libraryPath = Path.Combine (documentsPath, "..", "Library");
			string path = Path.Combine (libraryPath, _fileName);

			SQLitePlatformIOS platform = new SQLitePlatformIOS ();
			SQLiteConnection connection = new SQLiteConnection(platform, path);

			return connection;
		}

		public SQLiteAsyncConnection GetAsyncConnection ()
		{
			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			string libraryPath = Path.Combine (documentsPath, "..", "Library");
			string path = Path.Combine (libraryPath, _fileName);

			var platform = new SQLitePlatformIOS();

			var param = new SQLiteConnectionString(path, false); 
			var connection = new SQLiteAsyncConnection(() => new SQLiteConnectionWithLock(platform, param)); 

			return connection;

		}
	}
}


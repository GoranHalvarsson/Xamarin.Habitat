using HabitatApp.Droid.CrossDependencies;


[assembly: Xamarin.Forms.Dependency(typeof(SQLiteDb))]
namespace HabitatApp.Droid.CrossDependencies
{
	using System;
	using HabitatApp.CrossDependencies;
	using SQLite.Net;
	using SQLite.Net.Platform.XamarinAndroid;
	using System.IO;
	using SQLite.Net.Async;

	public class SQLiteDb : ISQLiteDb
	{
		public SQLiteDb ()
		{
		}

		public SQLiteConnection GetConnection ()
		{
			string fileName = "HabitatLocal.db3";
			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			string libraryPath = Path.Combine (documentsPath, "..", "Library");
			string path = Path.Combine (libraryPath, fileName);

			SQLitePlatformAndroid platform = new SQLitePlatformAndroid ();
			SQLiteConnection connection = new SQLiteConnection(platform, path);

			return connection;
		}

		public SQLiteAsyncConnection GetAsyncConnection (){
			string fileName = "HabitatLocal.db3";
			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			string applicationFolderPath = Path.Combine (documentsPath,"Habitat");
			string path = Path.Combine (applicationFolderPath, fileName);

			if(!Directory.Exists(applicationFolderPath))
				Directory.CreateDirectory(applicationFolderPath);
			
			SQLitePlatformAndroid platform = new SQLitePlatformAndroid();

			var param = new SQLiteConnectionString(path, false); 
			var connection = new SQLiteAsyncConnection(() => new SQLiteConnectionWithLock(platform, param)); 

			return connection;

		}
	}
}


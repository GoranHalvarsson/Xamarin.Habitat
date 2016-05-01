using HabitatApp.iOS.CrossDependencies;



[assembly: Xamarin.Forms.Dependency(typeof(FileService))]
namespace HabitatApp.iOS.CrossDependencies
{
	using HabitatApp.CrossDependencies;
	using System.IO;
	using System;

	public class FileService : IFileService
	{

		public string HabitatPath => Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "..", "Habitat");


		public string SaveFileToDisk (string id, byte[] fileData, string fileExtension)
		{
			if(!System.IO.Directory.Exists(HabitatPath))
				System.IO.Directory.CreateDirectory(HabitatPath);

			string path = Path.Combine (HabitatPath, $"{id}.{fileExtension}");

			if(File.Exists(path))
				return path;

			System.IO.File.WriteAllBytes(path, fileData);

			return path;

		}

		public bool FileExists (string id, string fileExtension)
		{
			string path = Path.Combine (HabitatPath, $"{id}.{fileExtension}");

			bool fileExists = File.Exists(path);

			return fileExists;
		}

		public void DeleteFile (string id, string fileExtension)
		{
			string path = Path.Combine (HabitatPath, $"{id}.{fileExtension}");

			File.Delete(path);
		
		}


		public FileService ()
		{
		}


	}
}


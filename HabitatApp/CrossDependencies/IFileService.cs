
namespace HabitatApp.CrossDependencies
{
	public interface IFileService
	{
		string SaveFileToDisk (string id, byte[] fileData, string fileExtension);

		bool FileExists (string id, string fileExtension);

		void DeleteFile (string id, string fileExtension);
	}
}


namespace HabitatApp.Services
{
	public interface ILoggingService
	{
		void ReportTime (string message);

		void LogTime (string message);

		void Log (string format, params object [] args);
	}
}


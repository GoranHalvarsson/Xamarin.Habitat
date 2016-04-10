namespace HabitatApp.Services
{

	using System;
	using System.Diagnostics;

	public class LoggingService : ILoggingService
	{

		private long lastTime;

		/// <summary>
		/// Reports the time.
		/// </summary>
		/// <param name="message">Message.</param>
		public void ReportTime (string message)
		{
			long now = DateTime.UtcNow.Ticks;

			Debug.WriteLine ("[{0}] ticks since last invoke: {1}", message, now-lastTime);
			lastTime = now;
		}

		/// <summary>
		/// Logs the time.
		/// </summary>
		/// <param name="message">Message.</param>
		public void LogTime (string message)
		{
			Debug.WriteLine ("[{0}] Time: {1}", message, DateTime.UtcNow);
		}

		/// <summary>
		/// Log the specified format and args.
		/// </summary>
		/// <param name="format">Format.</param>
		/// <param name="args">Arguments.</param>
		public void Log (string format, params object [] args)
		{
			Debug.WriteLine (String.Format (format, args));
		}
	}

}


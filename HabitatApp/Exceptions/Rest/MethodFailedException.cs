
namespace HabitatApp.Exceptions.Rest
{

	using System;
	using System.Net;

	/// <summary>
	/// Method failed exception.
	/// </summary>
	public class MethodFailedException : Exception
	{


		/// <summary>
		/// Initializes a new instance of the <see cref="HabitatApp.Exceptions.Rest.MethodFailedException"/> class.
		/// </summary>
		public MethodFailedException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HabitatApp.Exceptions.Rest.MethodFailedException"/> class.
		/// </summary>
		/// <param name="errorCode">Error code.</param>
		/// <param name="errorMessage">Error message.</param>
		public MethodFailedException(HttpStatusCode errorCode, string errorMessage):base(errorMessage)
		{
			ErrorCode = errorCode;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HabitatApp.Exceptions.Rest.MethodFailedException"/> class.
		/// </summary>
		/// <param name="errorCode">Error code.</param>
		/// <param name="errorMessage">Error message.</param>
		/// <param name="innerException">Inner exception.</param>
		public MethodFailedException(HttpStatusCode errorCode, string errorMessage, Exception innerException)
			: base(errorMessage, innerException)
		{
			ErrorCode = errorCode;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HabitatApp.Exceptions.Rest.MethodFailedException"/> class.
		/// </summary>
		/// <param name="errorMessage">Error message.</param>
		/// <param name="innerException">Inner exception.</param>
		public MethodFailedException(string errorMessage, Exception innerException) : base(errorMessage, innerException)
		{
			ErrorCode = HttpStatusCode.Unused;
		}


		/// <summary>
		/// Gets or sets the error code.
		/// </summary>
		/// <value>The error code.</value>
		public HttpStatusCode ErrorCode { get; protected set; }

	}
}




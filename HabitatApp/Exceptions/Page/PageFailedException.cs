
namespace HabitatApp.Exceptions.Page
{

	using System;
	using System.Net;

	/// <summary>
	/// Page failed exception.
	/// </summary>
	public class PageFailedException : Exception
	{


		/// <summary>
		/// Initializes a new instance of the <see cref="HabitatApp.Exceptions.Page.PageFailedException"/> class.
		/// </summary>
		public PageFailedException()
		{
			Page = "No page";
		}

		public PageFailedException(string errorMessage):base(errorMessage)
		{
			
		}
	
		public PageFailedException(string page, string errorMessage):base(errorMessage)
		{
			Page = page;
		}


		public PageFailedException(string page, string errorMessage, Exception innerException)
			: base(errorMessage, innerException)
		{
			Page = page;
		}



		/// <summary>
		/// Gets or sets the page.
		/// </summary>
		/// <value>The page.</value>
		public string Page { get; protected set; }

		/// <summary>
		/// Gets or sets the page.
		/// </summary>
		/// <value>The page.</value>
		public string PageType { get; protected set; }

	}
}




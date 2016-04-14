
namespace HabitatApp.Services
{
	using System;
	using System.Net.Http;
	using System.Threading.Tasks;
	using System.Threading;
	using System.Net;
	using System.Collections.Generic;
	using HabitatApp.Exceptions.Rest;

	/// <summary>
	/// Rest remote service.
	/// </summary>
	public class RestService : IRestService, IDisposable
	{
		private ILoggingService _loggingService;
		private HttpClient _httpClient;

		/// <summary>
		/// Initializes a new instance of the <see cref="HabitatApp.Services.RestService"/> class.
		/// </summary>
		/// <param name="loggingService">Logging service.</param>
		public RestService (ILoggingService loggingService)
		{
			this._loggingService = loggingService;
			this._httpClient = new HttpClient();

		}

		/// <summary>
		/// Gets the async.
		/// </summary>
		/// <returns>The async.</returns>
		/// <param name="methodName">Method name.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <param name="requestHeaders">Request headers.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public async Task<T> GetAsync<T>(string methodName, CancellationToken cancellationToken, IDictionary<string, string> requestHeaders = null) where T : class  
		{
			HttpRequestMessage requestMessage = CreateHttpRequestMessage(HttpMethod.Get, methodName, requestHeaders, null);

			HttpResponseMessage response = await SendAsync(requestMessage,cancellationToken);

			if (response.StatusCode != HttpStatusCode.OK)
				throw new MethodFailedException(response.StatusCode, response.ReasonPhrase);

			if(typeof(T) == typeof(String))
				return (T)Convert.ChangeType(await response.Content.ReadAsStringAsync(), typeof(T));

			if(typeof(T) == typeof(Byte[]))
				return (T)Convert.ChangeType(await response.Content.ReadAsByteArrayAsync(), typeof(T));

			return null;
		}



		/// <summary>
		/// Creates the http request message.
		/// </summary>
		/// <returns>The http request message.</returns>
		/// <param name="method">Method.</param>
		/// <param name="uriPathAndQuery">URI path and query.</param>
		/// <param name="requestHeaders">Request headers.</param>
		/// <param name="content">Content.</param>
		private HttpRequestMessage CreateHttpRequestMessage(HttpMethod method, string uriPathAndQuery, IDictionary<string, string> requestHeaders, HttpContent content)
		{
			HttpRequestMessage request = new HttpRequestMessage();

			if (requestHeaders == null) {
				requestHeaders = new Dictionary<string,string> ();
				requestHeaders.Add(new KeyValuePair<string, string>("ContentType","application/json"));
				requestHeaders.Add (new KeyValuePair<string, string> ("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)"));
			}


			// Set the Uri and Http Method
			request.RequestUri = new Uri(uriPathAndQuery);
			request.Method = method;

			// Add the user's headers
			if (requestHeaders != null)
			{
				foreach (KeyValuePair<string, string> header in requestHeaders)
				{
					request.Headers.Add(header.Key, header.Value);
				}
			}


			// Add the content
			if (content != null)
			{
				request.Content = content;
			}

			return request;
		}

		/// <summary>
		/// Sends the async.
		/// </summary>
		/// <returns>The async.</returns>
		/// <param name="request">Request.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			try
			{
				return await _httpClient.SendAsync(request, cancellationToken);
			}
			catch (TaskCanceledException ex)
			{
				this._loggingService.Log("Error in SendAsync,  Url {0} . Error: {1}", request.RequestUri.ToString(), ex.Message); 
				throw new MethodFailedException(HttpStatusCode.Unused, "Task Canceled", ex);
			}
			catch (Exception ex)
			{
				this._loggingService.Log("Error in SendAsync,  Url {0} . Error: {1}", request.RequestUri.ToString(), ex.Message);
				throw new MethodFailedException(HttpStatusCode.Unused, "Communication error, see innerexception", ex);
			}
		}

		public void Dispose()
		{
			_httpClient.Dispose();
			_httpClient = null;
		}
	}
}



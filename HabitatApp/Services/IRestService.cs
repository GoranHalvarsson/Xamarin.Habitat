namespace HabitatApp.Services
{
	using System.Threading.Tasks;
	using System.Threading;
	using System.Collections.Generic;
	using System.Net.Http;

	public interface IRestService
	{
		Task<T> GetAsync<T>(string methodName, CancellationToken cancellationToken, IDictionary<string, string> requestHeaders = null) where T: class; 

		Task<HttpResponseMessage> SendAsync (HttpRequestMessage request, CancellationToken cancellationToken);
	}
}



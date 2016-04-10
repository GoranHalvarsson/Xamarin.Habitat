


namespace HabitatApp.Services
{
	
	using System.Threading.Tasks;
	using Sitecore.MobileSDK.API.Items;
	using Sitecore.MobileSDK.API.Request.Parameters;
	using System.Collections.Generic;
	using HabitatApp.Models;

	public interface ISitecoreService
	{
		Task<ScItemsResponse> GetItemByPath(string itemPath, PayloadType itemLoadType, List<ScopeType> itemScopeTypes, string itemLanguage = "en");

		Task<ScItemsResponse> GetItemById (string itemId, PayloadType itemLoadType, List<ScopeType> itemScopeTypes, string itemLanguage = "en");

		Task<PageData> GeneratePageData(string itemid, PayloadType itemLoadType, List<ScopeType> itemScopeTypes, string datasourceFieldName, string itemLanguage = "en");
	

	
	}
}


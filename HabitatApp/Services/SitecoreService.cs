
namespace HabitatApp.Services
{

	using System;
	using Sitecore.MobileSDK.API.Items;
	using Sitecore.MobileSDK.API;
	using Sitecore.MobileSDK.API.Session;
	using Xamarin.Forms;
	using Sitecore.MobileSDK.PasswordProvider.Interface;
	using System.Threading.Tasks;
	using Sitecore.MobileSDK.API.Request;
	using Sitecore.MobileSDK.API.Request.Parameters;
	using System.Collections.Generic;
	using Sitecore.MobileSDK.API.Exceptions;
	using System.Linq;
	using HabitatApp.Repositories;
	using HabitatApp.Models;
	using HabitatApp.Extensions;
	using HabitatApp.CrossDependencies;



	public class SitecoreService : ISitecoreService
	{
		
		private readonly ISettingsRepository _settingsRepository;

		private readonly ILoggingService _loggingService;

	
		public SitecoreService (ISettingsRepository settingsRepository, ILoggingService loggingService)
		{
			_settingsRepository = settingsRepository;
			_loggingService = loggingService;
		}
		

		public async Task<ScItemsResponse> GetItemByPath(string itemPath, PayloadType itemLoadType, List<ScopeType> itemScopeTypes, string itemLanguage = "en"){

			try {


				using (ISitecoreWebApiSession session = await SitecoreSession) {
					IReadItemsByPathRequest request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath (itemPath)
						.Payload (itemLoadType)
						.AddScope (itemScopeTypes)
						.Language(itemLanguage)
						.Build ();


					return await session.ReadItemAsync(request);
				
				}
			} 
			catch(SitecoreMobileSdkException ex)
			{
				this._loggingService.Log ("Error in GetItemByPath,  id {0} . Error: {1}", itemPath, ex.Message); 
				throw ex;
			}
			catch(Exception ex)
			{
				this._loggingService.Log ("Error in GetItemByPath,  id {0} . Error: {1}", itemPath, ex.Message); 
				throw ex;
			}
				
	
		}


		public async Task<ScItemsResponse> GetItemById(string itemId, PayloadType itemLoadType, List<ScopeType> itemScopeTypes, string itemLanguage = "en"){

			try {

				using (ISitecoreWebApiSession session = await SitecoreSession) {
					IReadItemsByIdRequest request = ItemWebApiRequestBuilder.ReadItemsRequestWithId (itemId)
						.Payload (itemLoadType)
						.AddScope (itemScopeTypes)
						.Language(itemLanguage)
						.Build ();


					return await session.ReadItemAsync(request);

				}

			} 
			catch(SitecoreMobileSdkException ex)
			{
				this._loggingService.Log ("Error in GetItemById,  id {0} . Error: {1}", itemId, ex.Message); 
				throw ex;
			}
			catch(Exception ex)
			{
				this._loggingService.Log ("Error in GetItemById,  id {0} . Error: {1}", itemId, ex.Message); 
				throw ex;
			}


		}


		public async Task<PageData> GeneratePageData(string itemid, PayloadType itemLoadType, List<ScopeType> itemScopeTypes, string datasourceFieldName,  string itemLanguage = "en"){

			ScItemsResponse response = await GetItemById(itemid, itemLoadType, itemScopeTypes, itemLanguage);

			if (response == null)
				return null;

			ISitecoreItem item = response.First ();

			if (item == null)
				return null;

			PageData pageData = new PageData {
				PageName = item.DisplayName,
				ItemContext =  response,
				NavigationTitle = item.GetValueFromField(Constants.Sitecore.Fields.Navigation.NavigationTitle),
				PageType = item.GetTemplateName(),
				DataSourceFromChildren = await GetDatasourceFromChildren(item),
				DataSourceFromField = await GetDataSourceFromFieldName(item,datasourceFieldName) 

			};

			return pageData;
		}


		private async Task<IList<ScItemsResponse>> GetDataSourceFromFieldName(ISitecoreItem sitecoreItem, string fieldName){


			if (sitecoreItem.Fields.All(f => f.Name != fieldName))
				return null;

			string value = sitecoreItem [fieldName].RawValue;

			if (string.IsNullOrWhiteSpace (value))
				return null;

			string[] itemIds = value.Split('|');

			if (!itemIds.Any())
				return null;


			IList<ScItemsResponse> sitecoreItems = new List<ScItemsResponse> ();


			foreach (string itemId in itemIds) {


				ScItemsResponse response = await GetItemById(itemId, PayloadType.Content, new List<ScopeType>(){ScopeType.Self}, sitecoreItem.Source.Language);

				if (response == null)
					continue;

				sitecoreItems.Add (response);
			}

			return sitecoreItems;

		}

		private async Task<ScItemsResponse> GetDatasourceFromChildren(ISitecoreItem sitecoreItem){

			return await GetItemById(sitecoreItem.Id, PayloadType.Content, new List<ScopeType>(){ScopeType.Children}, sitecoreItem.Source.Language);

		}


		private Task<ISitecoreWebApiSession> SitecoreSession {
			get{ 
					return GetSession ();
				}
		}

		private async Task<ISitecoreWebApiSession> GetSession()
		{
			Models.Settings settings = await _settingsRepository.GetWithFallback ();

			using (IWebApiCredentials credentials = DependencyService.Get<ICustomSecureStringPasswordProvider> ().Login (settings.SitecoreUserName, settings.SitecorePassword)) {

				{
					ISitecoreWebApiSession session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost (settings.RestBaseUrl)
						.Credentials (credentials)
						.Site (settings.SitecoreShellSite)
						.DefaultDatabase (settings.SitecoreDefaultDatabase)
						.DefaultLanguage (settings.SitecoreDefaultLanguage)
						.MediaLibraryRoot (settings.SitecoreMediaLibraryRoot)
						.MediaPrefix (settings.SitecoreMediaPrefix)
						.DefaultMediaResourceExtension (settings.SitecoreDefaultMediaResourceExtension)
						.BuildSession ();

					return session;
				}
			}

		}




	}

}


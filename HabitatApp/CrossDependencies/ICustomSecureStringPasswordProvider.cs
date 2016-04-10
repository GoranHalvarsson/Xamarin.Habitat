namespace HabitatApp.CrossDependencies
{
	using Sitecore.MobileSDK.PasswordProvider.Interface;

	public interface ICustomSecureStringPasswordProvider
	{
		IWebApiCredentials Login(string userName, string password);
	}
}


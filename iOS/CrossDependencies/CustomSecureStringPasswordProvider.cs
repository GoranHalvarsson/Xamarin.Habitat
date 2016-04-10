using HabitatApp.iOS.CrossDependencies;

[assembly: Xamarin.Forms.Dependency(typeof(CustomSecureStringPasswordProvider))]
namespace HabitatApp.iOS.CrossDependencies
{
	using Sitecore.MobileSDK.PasswordProvider.Interface;
	using Sitecore.MobileSDK.PasswordProvider.iOS;
	using HabitatApp.CrossDependencies;

	public class CustomSecureStringPasswordProvider : ICustomSecureStringPasswordProvider
	{
		public IWebApiCredentials Login(string userName, string password){
			return new SecureStringPasswordProvider(userName, password);
		}
	}
}


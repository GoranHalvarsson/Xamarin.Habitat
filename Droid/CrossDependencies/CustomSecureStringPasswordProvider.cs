using HabitatApp.Droid.CrossDependencies;

[assembly: Xamarin.Forms.Dependency(typeof(CustomSecureStringPasswordProvider))]
namespace HabitatApp.Droid.CrossDependencies
{
	using Sitecore.MobileSDK.PasswordProvider.Interface;
	using Sitecore.MobileSDK.PasswordProvider.Android;
	using HabitatApp.CrossDependencies;

	public class CustomSecureStringPasswordProvider : ICustomSecureStringPasswordProvider
	{
		public IWebApiCredentials Login(string userName, string password){
			return new SecureStringPasswordProvider(userName, password);
		}
	}
}


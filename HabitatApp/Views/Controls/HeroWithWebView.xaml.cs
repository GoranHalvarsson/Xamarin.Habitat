
namespace HabitatApp.Views.Controls
{
	using System;
	using Xamarin.Forms;

	public partial class HeroWithWebView : ContentView
	{
		public HeroWithWebView ()
		{

			InitializeComponent ();

			Browser.Navigating += (s, e) =>
			{
				if (e.Url.StartsWith ("http", System.StringComparison.Ordinal)) {

					try {
						Uri uri = new Uri (e.Url);
						Device.OpenUri (uri);
					} catch (Exception) {
					}

					e.Cancel = true;
				}
			};


		}
	}
}


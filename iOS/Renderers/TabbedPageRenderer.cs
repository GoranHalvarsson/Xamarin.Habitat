using Xamarin.Forms;
using HabitatApp.iOS.Renderers;


//No need to this anymore, since the last [assembly: ExportRenderer(typeof(TabbedPage), typeof(TabbedPageRenderer))]
namespace HabitatApp.iOS.Renderers
{
	using System;
	using Xamarin.Forms.Platform.iOS;
	using UIKit;

	public class TabbedPageRenderer: TabbedRenderer 
	{
		public TabbedPageRenderer ()
		{

			TabBar.BarTintColor = UIColor.FromRGB(46, 56, 78);
			TabBar.BackgroundColor = UIColor.FromRGB(46, 56, 78);
			TabBar.SelectedImageTintColor = UIColor.White;

		}
	}
}


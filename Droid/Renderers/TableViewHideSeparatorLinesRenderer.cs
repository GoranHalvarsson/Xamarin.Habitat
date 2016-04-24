using Xamarin.Forms;
using HabitatApp.Views.Controls;
using HabitatApp.Droid.Renderers;

[assembly: ExportRenderer(typeof(TableViewHideSeparatorLines), typeof(TableViewHideSeparatorLinesRenderer))]
namespace HabitatApp.Droid.Renderers
{
	using Xamarin.Forms.Platform.Android;


	public class TableViewHideSeparatorLinesRenderer: TableViewRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<TableView> e)
		{
			base.OnElementChanged (e);

			if (Control == null)
				return;

			var listView = Control as global::Android.Widget.ListView;
			listView.DividerHeight = 0;
		}
	}
}


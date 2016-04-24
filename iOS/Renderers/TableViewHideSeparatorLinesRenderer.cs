using Xamarin.Forms;
using HabitatApp.iOS.Renderers;
using HabitatApp.Views.Controls;


[assembly: ExportRenderer(typeof(TableViewHideSeparatorLines), typeof(TableViewHideSeparatorLinesRenderer))]
namespace HabitatApp.iOS.Renderers
{

	using Xamarin.Forms.Platform.iOS;
	using UIKit;

	public class TableViewHideSeparatorLinesRenderer: TableViewRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<TableView> e)
		{
			base.OnElementChanged (e);

			if (Control == null)
				return;

			var tableView = Control as UITableView;
			tableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
		}
	}
}


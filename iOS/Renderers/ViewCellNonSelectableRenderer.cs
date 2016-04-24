using Xamarin.Forms;
using HabitatApp.Views.Controls;
using HabitatApp.iOS.Renderers;


[assembly: ExportRenderer(typeof(ViewCellNonSelectable), typeof(ViewCellNonSelectableRenderer))]
namespace HabitatApp.iOS.Renderers
{

	using UIKit;
	using Xamarin.Forms.Platform.iOS;

	public class ViewCellNonSelectableRenderer : StandardViewCellRenderer
	{
		public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			var cell = base.GetCell(item, reusableCell, tv);

			cell.SelectionStyle = UITableViewCellSelectionStyle.None;


			return cell;           

		}



	}
}


using Xamarin.Forms;
using HabitatApp.iOS.Renderers;


[assembly: ExportRenderer(typeof(ViewCell), typeof(StandardViewCellRenderer))]
namespace HabitatApp.iOS.Renderers
{

	using System;
	using Xamarin.Forms.Platform.iOS;

	public class StandardViewCellRenderer : ViewCellRenderer
	{

		public override UIKit.UITableViewCell GetCell (Cell item, UIKit.UITableViewCell reusableCell, UIKit.UITableView tv)
		{
			UIKit.UITableViewCell cell = base.GetCell (item, reusableCell, tv);
			switch (item.StyleId)
			{
			case "disclosure":
				cell.Accessory = UIKit.UITableViewCellAccessory.DisclosureIndicator;
				break;
			case "checkmark":
				cell.Accessory = UIKit.UITableViewCellAccessory.Checkmark;
				break;
			case "detail-button":
				cell.Accessory = UIKit.UITableViewCellAccessory.DetailButton;
				break;
			case "detail-disclosure-button":
				cell.Accessory = UIKit.UITableViewCellAccessory.DetailDisclosureButton;
				break;
			default:
				cell.Accessory = UIKit.UITableViewCellAccessory.None;
				break;
			}
			return cell;
		}

	}
}


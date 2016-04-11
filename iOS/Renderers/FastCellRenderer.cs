using Xamarin.Forms;
using HabitatApp.Views.Controls;
using HabitatApp.iOS.Renderers;


[assembly: ExportRenderer (typeof(FastCell), typeof(FastCellRenderer))]
namespace HabitatApp.iOS.Renderers
{
	using Xamarin.Forms.Platform.iOS;
	using Foundation;
	using UIKit;
	using System;


	public class FastCellRenderer : ViewCellRenderer
	{
		public FastCellRenderer ()
		{
		}

		private NSString _cellId;

		public override UITableViewCell GetCell (Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			_cellId = _cellId ?? new NSString (item.GetType ().FullName);

			var cellCache = FastCellCache.Instance.GetCellCache (tv);
			var fastCell = item as FastCell;
			var nativeCell = reusableCell as NativeCell;

			if (reusableCell != null && cellCache.IsCached (nativeCell)) {
				cellCache.RecycleCell (nativeCell, fastCell);
			} else {
				var newCell = (FastCell)Activator.CreateInstance (item.GetType ());
				newCell.BindingContext = item.BindingContext;
				newCell.Parent = item.Parent;				

				if (!newCell.IsInitialized) {
					newCell.PrepareCell ();
				}
				nativeCell = new NativeCell (_cellId, newCell);
				cellCache.CacheCell (newCell, nativeCell);
			}
			return nativeCell;
		}

	}
}


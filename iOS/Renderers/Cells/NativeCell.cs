


namespace HabitatApp.iOS.Renderers
{

	using UIKit;
	using HabitatApp.Views.Controls;
	using Xamarin.Forms.Platform.iOS;
	using CoreGraphics;
	using Xamarin.Forms;
	using Foundation;

	public class NativeCell : UITableViewCell
	{

		private UIView _view;
		private FastCell _fastCell;
		private CGSize _lastSize;

		public override string ToString ()
		{
			return $"[NativeCell: FC.BC={_fastCell.BindingContext}  FC.OBC={_fastCell.OriginalBindingContext}]";
		}


		public NativeCell (NSString cellId, FastCell fastCell) : base (UITableViewCellStyle.Default, cellId)
		{
			_fastCell = fastCell;
			_fastCell.PrepareCell ();
		
			IVisualElementRenderer renderer = Platform.CreateRenderer(fastCell.View);
			_view = renderer.NativeView;
			ContentView.AddSubview (_view);
		}

		public void RecycleCell (FastCell newCell)
		{
			if (newCell == _fastCell) {
				_fastCell.BindingContext = _fastCell.OriginalBindingContext;
			} else {
				_fastCell.BindingContext = newCell.BindingContext;
			}
			_fastCell.BindingContext = newCell.BindingContext;
		}



		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			//TODO update sizes of the xamarin view
			if (_lastSize.Equals (CGSize.Empty) || !_lastSize.Equals (Frame.Size)) {

				Layout<View> layout = _fastCell.View as Layout<View>;
				if (layout != null) {
					layout.Layout (Frame.ToRectangle ());
					layout.ForceLayout ();
					FixChildLayouts (layout);
				}
				_lastSize = Frame.Size;
			}

			_view.Frame = ContentView.Bounds;
		}

		void FixChildLayouts (Layout<View> layout)
		{
			foreach (var child in layout.Children) {
				if (child is StackLayout) {
					((StackLayout)child).ForceLayout ();
					FixChildLayouts (child as Layout<View>);
				}
				if (child is AbsoluteLayout) {
					((AbsoluteLayout)child).ForceLayout ();
					FixChildLayouts (child as Layout<View>);
				}
			}
		}
	}
}


using Xamarin.Forms;
using HabitatApp.Views.Controls.Carousel;
using HabitatApp.Droid.Renderers;


[assembly:ExportRenderer (typeof(CarouselLayout), typeof(CarouselLayoutRenderer))]
namespace HabitatApp.Droid.Renderers
{

	using System;
	using Xamarin.Forms.Platform.Android;
	using System.Timers;
	using Android.Widget;
	using System.ComponentModel;
	using System.Reflection;
	using Android.Views;
	using Android.Graphics;
	using Java.Lang;

	public class CarouselLayoutRenderer : ScrollViewRenderer
	{
		int _prevScrollX;
		int _deltaX;
		bool _motionDown;
		Timer _deltaXResetTimer;
		Timer _scrollStopTimer;
		//HorizontalScrollView _scrollView;

		protected override void OnElementChanged (VisualElementChangedEventArgs e)
		{
			base.OnElementChanged (e);
			if (e.NewElement == null)
				return;

			_deltaXResetTimer = new Timer (100) { AutoReset = false };
			_deltaXResetTimer.Elapsed += (object sender, ElapsedEventArgs args) => _deltaX = 0;

			_scrollStopTimer = new Timer (200) { AutoReset = false };
			_scrollStopTimer.Elapsed += (object sender, ElapsedEventArgs args2) => UpdateSelectedIndex ();

			e.NewElement.PropertyChanged += ElementPropertyChanged;
		}

		void ElementPropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Renderer") {
//				_scrollView = (HorizontalScrollView)typeof(ScrollViewRenderer)
//					.GetField ("hScrollView", BindingFlags.NonPublic | BindingFlags.Instance)
//					.GetValue (this);



				this.HorizontalScrollBarEnabled = false;
//				this.Touch += HScrollViewTouch;
			}
			if (e.PropertyName == CarouselLayout.SelectedIndexProperty.PropertyName && !_motionDown) {
				ScrollToIndex (((CarouselLayout)this.Element).SelectedIndex);
			}
		}

		public override bool DispatchTouchEvent (MotionEvent e)
		{

			switch (e.Action) {
			case MotionEventActions.Move:
				_deltaXResetTimer.Stop ();
				_deltaX = GetScrollIndex () - _prevScrollX;
				_prevScrollX = GetScrollIndex ();

				UpdateSelectedIndex ();

				_deltaXResetTimer.Start ();
				break;
			case MotionEventActions.Down:
				_motionDown = true;
				_scrollStopTimer.Stop ();
				break;
			case MotionEventActions.Up:
				_motionDown = false;
				SnapScroll ();
				_scrollStopTimer.Start ();
				break;
			}


			return base.DispatchTouchEvent (e);
		}

		private Int32 GetScrollIndex ()
		{
			return Convert.ToInt32 (((CarouselLayout)(this.Element)).ScrollX);
		}

		void HScrollViewTouch (object sender, TouchEventArgs e)
		{
			e.Handled = false;

			switch (e.Event.Action) {
			case MotionEventActions.Move:
				_deltaXResetTimer.Stop ();
				_deltaX = this.ScrollX - _prevScrollX;
				_prevScrollX = this.ScrollX;

				UpdateSelectedIndex ();

				_deltaXResetTimer.Start ();
				break;
			case MotionEventActions.Down:
				_motionDown = true;
				_scrollStopTimer.Stop ();
				break;
			case MotionEventActions.Up:
				_motionDown = false;
				SnapScroll ();
				_scrollStopTimer.Start ();
				break;
			}
		}

		void UpdateSelectedIndex ()
		{
			var center = GetScrollIndex () + (this.Width / 2);
			var carouselLayout = (CarouselLayout)this.Element;
			carouselLayout.SelectedIndex = (center / this.Width);
		}

		void SnapScroll ()
		{
			var roughIndex = (float)GetScrollIndex () / this.Width;

			var targetIndex =
				_deltaX < 0 ? Java.Lang.Math.Floor (roughIndex)
				: _deltaX > 0 ? Java.Lang.Math.Ceil (roughIndex)
				: Java.Lang.Math.Round (roughIndex);

			ScrollToIndex ((int)targetIndex);
		}

		void ScrollToIndex (int targetIndex)
		{
			var targetX = targetIndex * this.Width;
			this.Post (new Runnable (() => {
				this.SmoothScrollTo (targetX, 0);
			}));
		}

		bool _initialized = false;

		public override void Draw (Canvas canvas)
		{
			base.Draw (canvas);
			if (_initialized)
				return;
			_initialized = true;
			var carouselLayout = (CarouselLayout)this.Element;
			this.ScrollTo (carouselLayout.SelectedIndex * Width, 0);
		}

		protected override void OnSizeChanged (int w, int h, int oldw, int oldh)
		{
			if (_initialized && (w != oldw)) {
				_initialized = false;
			}
			base.OnSizeChanged (w, h, oldw, oldh);
		}
	}
}


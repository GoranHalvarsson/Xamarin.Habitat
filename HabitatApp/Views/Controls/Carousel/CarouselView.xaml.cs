


namespace HabitatApp.Views.Controls.Carousel
{
	
	using Xamarin.Forms;
	using Xamarin.Forms.Xaml;
	using System.Threading.Tasks;


	[XamlCompilation (XamlCompilationOptions.Compile)]
	public partial class CarouselView : ContentView
	{
		public CarouselView ()
		{
			InitializeComponent ();
		}

		private Color _dotColor = Color.Black;
		public Color DotColor {
			get { return _dotColor;}
			set { _dotColor = value;
				dotLayout.DotColor = value;

			}
		}

		private Color _dotBorderColor = Color.Black;
		public Color DotBorderColor {
			get { return _dotBorderColor;}
			set { _dotBorderColor = value;
				dotLayout.DotBorderColor = value;

			}
		}

		private double _dotSize =5;
		public double DotSize {
			get { return _dotSize;}
			set { _dotSize = value;
				dotLayout.DotSize = value;

			}
		}

		private double _dotBorderWidth = 0;
		public double DotBorderWidth {
			get { return _dotBorderWidth;}
			set { _dotBorderWidth = value;
				dotLayout.DotBorderWidth = value;

			}
		}

		private DataTemplate _itemTemplate;
		public DataTemplate ItemTemplate {
			get { return _itemTemplate;}
			set {

				_itemTemplate=value;

				carouselLayout.ItemTemplate = value;
			}
		}

		public int SelectedIndex {
			get {

				return (int)carouselLayout.SelectedIndex;
			}
		}

		public static readonly BindableProperty ScrollToIndexProperty =
			BindableProperty.Create<CarouselView, int> (
				carousel => carousel.ScrollToIndex,
				0,
				BindingMode.TwoWay,
				propertyChanged: async (bindable, oldValue, newValue) => {
					await ((CarouselView)bindable).ScrollToIndexCommand (newValue);

				}
			);

		public int ScrollToIndex {
			get {
				return (int)GetValue (ScrollToIndexProperty);
			}
			set {
				SetValue (ScrollToIndexProperty, value);
			}
		}

		public async Task ScrollToIndexCommand (int index) {
			var el = carouselLayout.Children[index];
			await carouselLayout.ScrollToAsync (el, ScrollToPosition.Start, true);
		}
	}

}


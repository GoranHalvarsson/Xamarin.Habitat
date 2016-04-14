
namespace HabitatApp.Models
{

	using Xamarin.Forms;

	public class ListItem: ObservableModel
	{

		private Color _backgroundColor;

		public Color BackgroundColor {
			get {
				return _backgroundColor;
			}
			set { SetProperty (ref _backgroundColor, value); }

		}

		private string _header;

		public string Header {
			get {
				return _header;
			}
			set { SetProperty (ref _header, value); }

		}

		private string _text;

		public string Text {
			get {
				return _text;
			}
			set { SetProperty (ref _text, value); }

		}

		private string _navigationItem;

		public string NavigationItem {
			get {
				return _navigationItem;
			}
			set { SetProperty (ref _navigationItem, value); }

		}

		private string _navigationText;

		public string NavigationText {
			get {
				return _navigationText;
			}
			set { SetProperty (ref _navigationText, value); }

		}

		private string _navigationUrl;

		public string NavigationUrl {
			get {
				return _navigationUrl;
			}
			set { SetProperty (ref _navigationUrl, value); }

		}


		private CachedMedia _media;

		public CachedMedia Media {
			get {
				return _media;
			}
			set { SetProperty (ref _media, value); }

		}
	}
}


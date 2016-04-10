
namespace HabitatApp.Models
{
	using Xamarin.Forms;
	using System;


	public class NavigationMenuItem : ObservableModel
	{

	
		public string Title { get; set; }

		public string IconSource { get; set; }

		public string PageUrl { get; set; }

		public string ItemId { get; set; }

		private Color _rowColor;
		public Color RowColor {
			get {
				return _rowColor;
			}
			set { SetProperty (ref _rowColor, value); }

		}

		public PageData PageContext { get; set; }

		public bool ShowInMenu { get; set; }
	}
}



namespace HabitatApp.Views.Controls
{
	using System;

	using Xamarin.Forms;
	using System.Diagnostics;


	public class FastImage : Image
	{
		public FastImage ()
		{

		}

		public static readonly BindableProperty ImageUrlProperty = BindableProperty.Create<FastImage, string> (w => w.ImageUrl, null);

		/// <summary>
		/// sets the image URL.
		/// </summary>
		/// <value>The image URL.</value>
		public string ImageUrl {
			get { return (string)GetValue (ImageUrlProperty); }
			set { 
				SetValue (ImageUrlProperty, value);
			}
		}
	}




}


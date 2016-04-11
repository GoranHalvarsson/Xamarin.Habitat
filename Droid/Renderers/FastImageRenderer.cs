using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using HabitatApp.Views.Controls;
using com.refractored.monodroidtoolkit.imageloader;

namespace HabitatApp.Droid.Renderers
{
	public class FastImageRenderer : ImageRenderer
	{
		ImageLoader _imageLoader;

		protected override void OnElementChanged (ElementChangedEventArgs<Image> e)
		{
			base.OnElementChanged (e);
			if (e.NewElement != null) {
				var fastImage = e.NewElement as FastImage;
				_imageLoader = ImageLoaderCache.GetImageLoader (this);
				SetImageUrl (fastImage.ImageUrl);
			}
		}

		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);
			if (e.PropertyName == "ImageUrl") {
				var fastImage = Element as FastImage;
				SetImageUrl (fastImage.ImageUrl);
			}
		}


		public void SetImageUrl (string imageUrl)
		{
			if (Control == null) {
				return;
			}
			if (imageUrl != null) {
				_imageLoader.DisplayImage (imageUrl, Control, -1);

			}
		}
	}

}


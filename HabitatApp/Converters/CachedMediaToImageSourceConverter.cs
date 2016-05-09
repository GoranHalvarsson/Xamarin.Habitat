namespace HabitatApp.Converters
{
	using Xamarin.Forms;
	using System;
	using System.Globalization;
	using HabitatApp.Models;
	using System.IO;
	using Xamarin.Forms.Xaml;


	public class CachedMediaToImageSourceConverter : IMarkupExtension, IValueConverter
	{

		public  object Convert (object value, Type targetType, object parameter, CultureInfo culture)
		{
			CachedMedia cachedMedia = (CachedMedia)value;

			if (cachedMedia == null)
				return null;    

			if (cachedMedia.MediaData != null)
				return ImageFromBytes (cachedMedia);

			return ImageFromUrl (cachedMedia.Url);

		}

		private ImageSource ImageFromBytes (CachedMedia cachedMedia)
		{
			return ImageSource.FromStream (() => new MemoryStream (cachedMedia.MediaData));
		}

		private ImageSource ImageFromUrl (string url)
		{

			Uri outUri;

			if (Uri.TryCreate (url, UriKind.Absolute, out outUri))
				return ImageSource.FromUri (outUri);

			return ImageSource.FromResource (url);
		}


		public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException ();
		}


		public object ProvideValue (IServiceProvider serviceProvider)
		{
			return this;
		}

	}

}



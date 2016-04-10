namespace HabitatApp.Converters
{
	using Xamarin.Forms;
	using System;
	using System.Globalization;


	public class ImageUrlToImageSourceConverter : IValueConverter
	{
		
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string imgUrl = (string)value;

			if (string.IsNullOrWhiteSpace(imgUrl))
				return null;	

	
			return ImageFromUrl (imgUrl);
		}


		private ImageSource ImageFromUrl(string url){

			Uri outUri;

			if (Uri.TryCreate(url, UriKind.Absolute, out outUri))
				return ImageSource.FromUri(outUri);

			return ImageSource.FromResource(url);
		}



		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}

	}

}


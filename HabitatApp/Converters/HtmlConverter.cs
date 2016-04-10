

namespace HabitatApp.Converters
{
	using System;
	using Xamarin.Forms;
	using HtmlAgilityPack;
	using HabitatApp.Extensions;

	public class HtmlConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{

			if(value == null)
				return string.Empty;

			string htmlContent = string.Empty;

			if(value is HtmlNode)
				htmlContent = ((HtmlNode)value).OuterHtml;

			if(value is string)
				htmlContent = (string)value;

			int charachterlength;

			if (!int.TryParse(parameter as string, out charachterlength))
				charachterlength = 99999;

			return TruncateText(htmlContent, charachterlength);;
		}

		public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException ();
		}



		private static string TruncateText(string htmlContent, int charachterlength){

			HtmlDocument htmlDoc = new HtmlDocument();
			htmlDoc.LoadHtml(htmlContent);

			if (htmlDoc == null || htmlDoc.DocumentNode == null)
				return string.Empty;

			string output = HtmlEntity.DeEntitize (htmlDoc.DocumentNode.InnerText);

			if(output.Length < charachterlength)
				return output;

			return output.CropWholeWords (charachterlength);





		}




	}
}


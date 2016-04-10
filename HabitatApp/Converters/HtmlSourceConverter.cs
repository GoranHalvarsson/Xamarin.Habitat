namespace HabitatApp.Converters
{
	using System;
	using Xamarin.Forms;
	using System.Globalization;

	public class HtmlSourceConverter : IValueConverter
	{

		private const string htmlString = @"<html>
<head>
<style type=""text/css"">
p {{
    margin: 0 0 10px;
}}
</style>
</head>
<body style=""background-color:{0};font-family: Bitter; font-size: 20px; color:{1}"">
{2}
</body>
</html>";


		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			HtmlWebViewSource html = new HtmlWebViewSource();

			string[] parameters = parameter.ToString ().Split (',');

			if (!string.IsNullOrWhiteSpace(value.ToString()))
			{
				html.Html = string.Format(htmlString, parameters[0], parameters[1], value.ToString());
			}

			return html;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}


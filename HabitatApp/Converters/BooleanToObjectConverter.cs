namespace HabitatApp.Converters
{
	using System;
	using Xamarin.Forms;
	using System.Globalization;

	public class BooleanToObjectConverter<T> : IValueConverter
	{
		public T FalseObject { set; get; }

		public T TrueObject { set; get; }

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ((bool?)value) == null ? this.TrueObject : (((bool?)value).Value ? this.TrueObject : this.FalseObject);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ((T)value).Equals(this.TrueObject);
		}
	}
}


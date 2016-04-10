namespace HabitatApp.Extensions
{
	using System.Collections.ObjectModel;
	using System.Collections.Generic;

	public static class IEnumerableExtensions
	{
		public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> col)
		{
			return new ObservableCollection<T>(col);
		}
	}
}


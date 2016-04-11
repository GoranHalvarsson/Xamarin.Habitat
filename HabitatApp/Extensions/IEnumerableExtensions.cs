
namespace HabitatApp.Extensions
{
	using System.Collections.ObjectModel;
	using System.Collections.Generic;
	using System;
	using System.Linq;


	public static class IEnumerableExtensions
	{
		public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> col)
		{
			return new ObservableCollection<T>(col);
		}

		public static IEnumerable<Tuple<T, T>> AsPairsSafe<T>(this List<T> list)
		{
			int index = 0;

			while (index < list.Count())
			{
				if (index + 1 >= list.Count())
					yield break;

				yield return new Tuple<T,T>(list[index++],  list[index++]);
			}

		}
	}
}


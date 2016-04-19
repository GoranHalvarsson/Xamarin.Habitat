namespace HabitatApp.Models
{
	using System;
	using System.Collections.ObjectModel;
	using System.Collections.Generic;

	public class GroupingCollection<K, T> : ObservableCollection<T> 
	{ 
		public GroupingCollection(K key, IEnumerable<T> items) { 
			Key = key; foreach (var item in items) this.Items.Add(item); 
		}

		public K Key { 
			get; private set; 
		} 
	} 
}


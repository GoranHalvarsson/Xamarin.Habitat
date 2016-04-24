namespace HabitatApp.Behaviors
{

	using System;
	using Xamarin.Forms;
	using Plugin.Connectivity;

	public class UrlValidationBehavior : Behavior<Entry>
	{

		private static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool?), typeof(UrlValidationBehavior), null);
		public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

		public static readonly BindableProperty UrlEntryProperty = BindableProperty.Create("UrlEntry", typeof(Entry), typeof(UrlValidationBehavior), null);

		public Entry UrlEntry
		{
			get { return (Entry)base.GetValue(UrlEntryProperty); }
			set { base.SetValue(UrlEntryProperty, value); }
		}

		protected override void OnAttachedTo(Entry bindable)
		{
			bindable.Unfocused += OnEntryFocusChanged;
			base.OnAttachedTo(bindable);
		}


		protected override void OnDetachingFrom(Entry bindable)
		{
			bindable.Unfocused -= OnEntryFocusChanged;
			base.OnDetachingFrom(bindable);
		}

		public bool? IsValid
		{
			get { return (bool?)base.GetValue(IsValidProperty); }
			private set { base.SetValue(IsValidPropertyKey, value); }
		}

		private async void OnEntryFocusChanged(object sender, FocusEventArgs e)
		{
			if (CrossConnectivity.Current.IsConnected )
				IsValid  = !string.IsNullOrWhiteSpace(UrlEntry.Text) && await CrossConnectivity.Current.IsRemoteReachable (UrlEntry.Text,80,2000);
			else
				IsValid = null;

		
			((Entry)sender).TextColor = IsValid.HasValue ? (IsValid.Value ? Color.Green : Color.Red) : Color.Default;
		}
	}
}


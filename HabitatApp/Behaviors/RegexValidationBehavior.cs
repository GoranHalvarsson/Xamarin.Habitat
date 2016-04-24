namespace HabitatApp.Behaviors
{

	using System;
	using Xamarin.Forms;
	using System.Text.RegularExpressions;

	public class RegexValidationBehavior : Behavior<Entry>
	{

		private static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool?), typeof(UrlValidationBehavior), null);
		public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

		public string RegexEntry {
			get; 
			set; 
		}

		protected override void OnAttachedTo(Entry bindable)
		{
			bindable.TextChanged += HandleTextChanged;
			base.OnAttachedTo(bindable);
		}


		protected override void OnDetachingFrom(Entry bindable)
		{
			bindable.TextChanged -= HandleTextChanged;
			base.OnDetachingFrom(bindable);
		}

		public bool? IsValid
		{
			get { return (bool?)base.GetValue(IsValidProperty); }
			private set { base.SetValue(IsValidPropertyKey, value); }
		}

		private void HandleTextChanged(object sender, TextChangedEventArgs e)
		{
			
			IsValid  = (Regex.IsMatch(e.NewTextValue, @RegexEntry, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));

		
			((Entry)sender).TextColor = IsValid.HasValue ? (IsValid.Value ? Color.Green : Color.Red) : Color.Default;
		}
	}
}


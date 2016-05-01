namespace HabitatApp.Views.Controls
{

	using System;

	using Xamarin.Forms;
	using System.Collections;

	public class EventArgs<T> : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EventArgs"/> class.
		/// </summary>
		/// <param name="value">Value of the argument</param>
		public EventArgs(T value)
		{
			this.Value = value;
		}

		/// <summary>
		/// Gets the value of the event argument
		/// </summary>
		public T Value { get; private set; }
	}

	public class CustomGridView : ContentView
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CustomGridView"/> class.
		/// </summary>
		public CustomGridView ()
		{
			SelectionEnabled = true;
		}


		//
		// Static Fields
		//
		/// <summary>
		/// The items source property
		/// </summary>
		public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create ("ItemsSource", typeof(IEnumerable), typeof(CustomGridView), null, BindingMode.OneWay, null, null, null, null);

		/// <summary>
		/// The item template property
		/// </summary>
		public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create ("ItemTemplate", typeof(DataTemplate), typeof(CustomGridView), null, BindingMode.OneWay, null, null, null, null);

		/// <summary>
		/// The row spacing property
		/// </summary>
		public static readonly BindableProperty RowSpacingProperty = BindableProperty.Create ("RowSpacing", typeof(double), typeof(CustomGridView), (double)0, BindingMode.OneWay, null, null, null, null);

		/// <summary>
		/// The column spacing property
		/// </summary>
		public static readonly BindableProperty ColumnSpacingProperty = BindableProperty.Create ("ColumnSpacing", typeof(double), typeof(CustomGridView), (double)0, BindingMode.OneWay, null, null, null, null);

		/// <summary>
		/// The item width property
		/// </summary>
		public static readonly BindableProperty ItemWidthProperty = BindableProperty.Create ("ItemWidth", typeof(double), typeof(CustomGridView), (double)100, BindingMode.OneWay, null, null, null, null);

		/// <summary>
		/// The item height property
		/// </summary>
		public static readonly BindableProperty ItemHeightProperty = BindableProperty.Create ("ItemHeight", typeof(double), typeof(CustomGridView), (double)100, BindingMode.OneWay, null, null, null, null);

		//
		// Properties
		//
		/// <summary>
		/// Gets or sets the items source.
		/// </summary>
		/// <value>The items source.</value>
		public IEnumerable ItemsSource {
			get {
				return (IEnumerable)base.GetValue (CustomGridView.ItemsSourceProperty);
			}
			set {
				base.SetValue (CustomGridView.ItemsSourceProperty, value);
			}
		}

		/// <summary>
		/// Gets or sets the item template.
		/// </summary>
		/// <value>The item template.</value>
		public DataTemplate ItemTemplate {
			get {
				return (DataTemplate)base.GetValue (CustomGridView.ItemTemplateProperty);
			}
			set {
				base.SetValue (CustomGridView.ItemTemplateProperty, value);
			}
		}

		/// <summary>
		/// Gets or sets the row spacing.
		/// </summary>
		/// <value>The row spacing.</value>
		public double RowSpacing {
			get {
				return (double)base.GetValue (CustomGridView.RowSpacingProperty);
			}
			set {
				base.SetValue (CustomGridView.RowSpacingProperty, value);
			}
		}

		/// <summary>
		/// Gets or sets the column spacing.
		/// </summary>
		/// <value>The column spacing.</value>
		public double ColumnSpacing {
			get {
				return (double)base.GetValue (CustomGridView.ColumnSpacingProperty);
			}
			set {
				base.SetValue (CustomGridView.ColumnSpacingProperty, value);
			}
		}

		/// <summary>
		/// Gets or sets the width of the item.
		/// </summary>
		/// <value>The width of the item.</value>
		public double ItemWidth {
			get {
				return (double)base.GetValue (CustomGridView.ItemWidthProperty);
			}
			set {
				base.SetValue (CustomGridView.ItemWidthProperty, value);
			}
		}

		/// <summary>
		/// Gets or sets the height of the item.
		/// </summary>
		/// <value>The height of the item.</value>
		public double ItemHeight {
			get {
				return (double)base.GetValue (CustomGridView.ItemHeightProperty);
			}
			set {
				base.SetValue (CustomGridView.ItemHeightProperty, value);
			}
		}

		/// <summary>
		/// Occurs when item is selected.
		/// </summary>
		public event EventHandler<EventArgs<object>> ItemSelected;

		/// <summary>
		/// Invokes the item selected event.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="item">Item.</param>
		public void InvokeItemSelectedEvent (object sender, object item)
		{
			if (this.ItemSelected != null) {
				this.ItemSelected.Invoke (sender, new EventArgs<object> (item));
			}

		}

		/// <summary>
		/// Gets or sets a value indicating whether [selection enabled].
		/// </summary>
		/// <value><c>true</c> if [selection enabled]; otherwise, <c>false</c>.</value>
		public bool SelectionEnabled {
			get;
			set;
		}
	}
}



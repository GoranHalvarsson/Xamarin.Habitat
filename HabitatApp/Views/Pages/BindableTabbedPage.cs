namespace HabitatApp.Views.Pages
{

	using HabitatApp.ViewModels;
	using Xamarin.Forms;
	using System.Collections;
	using System.Collections.Generic;

	public class BindableTabbedPage : TabbedPage
	{
		public BindableTabbedPage()
		{
			
		}


		public static BindableProperty ChildrenListProperty = BindableProperty.Create ("ChildrenList", 
			typeof(IList), typeof(BindableTabbedPage), 
			null, 
			BindingMode.Default, 
			null, 
			new BindableProperty.BindingPropertyChangedDelegate(UpdateList));

		public IList<Page> ChildrenList
		{
			get { return (IList<Page>)GetValue(ChildrenListProperty); }
			set { 
				SetValue(ChildrenListProperty, value); 
			}
		}

		private static void UpdateList(BindableObject bindable, object oldvalue, object newvalue)
		{

			BindableTabbedPage bindableTabbedPage = bindable as BindableTabbedPage;
			if (bindableTabbedPage == null) {
				return;
			}

			bindableTabbedPage.Children.Clear ();

			IList<Page> pageList = (IList<Page>)newvalue;

			if (pageList == null || pageList.Count == 0) {
				return;
			}

			foreach (Page page in pageList) {

				IViewModel model = (IViewModel)page.BindingContext;
				model.LoadAsync ().GetAwaiter();

				NavigationPage navPage = new NavigationPage (page);
				navPage.BarBackgroundColor = Color.FromRgb (46, 56, 78);
				navPage.BarTextColor = Color.White;
				navPage.Icon =  page.Icon;
				navPage.Title = page.Title;
				bindableTabbedPage.Children.Add (navPage);

			}
	
		}		

	}
}



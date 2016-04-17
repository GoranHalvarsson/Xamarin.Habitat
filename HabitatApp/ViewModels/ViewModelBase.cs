

namespace HabitatApp.ViewModels
{
	using System;
	using HabitatApp.Models;
	using Xamarin.Forms;
	using System.Threading.Tasks;


	public class ViewModelBase : ObservableModel, IViewModel
	{


		private PageData _pageData;


		public PageData PageContext { 
			get { return _pageData; } 
			set { SetProperty (ref _pageData, value); } 
		}


		private string _title;

		public string Title { 
			get { return _title; } 
			set { SetProperty(ref _title, value); }
		}


		private bool _isBusy;

		public bool IsBusy
		{
			get { return _isBusy; }
			set { SetProperty(ref _isBusy, value); }
		}


		private string _busyText;

		public string BusyText
		{
			get { return _busyText; }
			set { SetProperty(ref _busyText, value); }
		}


		private Page _connectedToPage;


		public Page ConnectedToPage
		{
			get { return _connectedToPage; }
			set { SetProperty(ref _connectedToPage, value); }
		}

		protected void SetBusy(string text)
		{
			BusyText = text;
			IsBusy = true;
		}

		protected void ClearBusy()
		{
			IsBusy = false;
			BusyText = string.Empty;
		}

		public virtual Task LoadAsync ()
		{
			throw new NotImplementedException ();
		}

		public virtual Task UnLoadAsync ()
		{
			throw new NotImplementedException ();
		}

	}
}


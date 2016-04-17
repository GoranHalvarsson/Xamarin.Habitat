using System;


namespace HabitatApp.Views.Controls
{
	using HabitatApp.Models;

	public partial class ViewFastCell : FastCell
	{
		protected override void InitializeCell ()
		{
			InitializeComponent ();
		}

		protected override void SetupCell (bool isRecycled)
		{
			ListItem listItem = BindingContext as ListItem;

			if (listItem == null)
				return;


//			UserThumbnailView.ImageUrl = listItem.Item1.ImageUrl ?? "";
//			ImageView.Source = listItem.Item1.ImageUrl ?? "";
//			NameLabel.Text = listItem.Item1.Header;
//			DescriptionLabel.Text = listItem.Item1.Text;
//			UserThumbnailView2. ImageUrl = listItem.Item1.ImageUrl ?? "";
//			UserThumbnailView3.ImageUrl = listItem.Item1.ImageUrl ?? "";
//			UserThumbnailView4.ImageUrl = listItem.Item1.ImageUrl ?? "";
//			UserThumbnailView5.ImageUrl = listItem.Item1.ImageUrl ?? "";
//			UserThumbnailView6.ImageUrl = listItem.Item1.ImageUrl ?? "";
//			UserThumbnailView7.ImageUrl = listItem.Item1.ImageUrl ?? "";

		}
	}
}


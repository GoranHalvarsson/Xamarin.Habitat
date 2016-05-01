namespace HabitatApp.Extensions
{
	using System;
	using Xamarin.Forms;
	using Xamarin.Forms.Xaml;

	[ContentProperty("Source")]
	public class ImageResourceExtension : IMarkupExtension
	{
		public string Source { get; set; }

		public object ProvideValue(IServiceProvider serviceProvider)
		{
			if (Source == null)
				return null;



//			ImageSource imageSource = null;
//
//			var transformer = Dependency.Resolve<IImageUrlTransformer, ImageUrlTransformer>(true);
//			string url = transformer.TransformForCurrentPlatform(Source);
//
//			if (Device.OS == TargetPlatform.Android)
//			{
//				imageSource = ImageSource.FromFile(url);
//			}

			return ImageSource.FromResource(Source);
		}
	}
}




namespace HabitatApp.Extensions
{
	using System;
	using Sitecore.MobileSDK.API.Items;
	using System.Xml.Linq;
	using System.Linq;
	using System.Text;

	public static class SitecoreItemExtensions
	{

		public static string GetValueFromField(this ISitecoreItem item, string fieldName){

			if (item == null)
				return string.Empty;

			return item [fieldName].RawValue;

		}

		public static string GetTemplateName(this ISitecoreItem item){

			if (item == null)
				return string.Empty;

			return item.Template.Substring (item.Template.LastIndexOf ("/", System.StringComparison.Ordinal)+1);

		}

		public static bool GetCheckBoxValueFromField(this ISitecoreItem item, string fieldName){

			if (item == null)
				return false;

			return item [fieldName].RawValue == "1";

		}

		public static string GetImageUrlFromMediaField(this ISitecoreItem item, string mediafieldName, string defaultImageUrl = "http://myhabitat.dev/-/media/Habitat/Images/Wide/Habitat-004-wide.jpg"){


			XElement xmlElement = GetXElement (item, mediafieldName);

			if (xmlElement == null)
				return defaultImageUrl;

			XAttribute attribute = xmlElement.Attributes().FirstOrDefault(attr => attr.Name == "mediaid");

			string mediaId = attribute.Value;

			Guid id = Guid.Parse (mediaId);

			StringBuilder builder = new StringBuilder ("http://myhabitat.dev");
			builder.AppendFormat ("/-/media/{0}.ashx", id.ToString ("N"));

			return builder.ToString ();

		}

		public static string GetItemIdFromLinkField(this ISitecoreItem item, string linkFieldName){


			XElement xmlElement = GetXElement (item, linkFieldName);

			if (xmlElement == null)
				return string.Empty;


			XAttribute attribute = xmlElement.Attributes().FirstOrDefault(attr => attr.Name == "id");

			return attribute.Value;

		}

		public static string GetTextFromLinkField(this ISitecoreItem item,  string linkFieldName){


			XElement xmlElement = GetXElement (item, linkFieldName);

			if (xmlElement == null)
				return string.Empty;


			XAttribute attribute = xmlElement.Attributes().FirstOrDefault(attr => attr.Name == "text");

			return attribute.Value;

		}

		private static XElement GetXElement (this ISitecoreItem item, string fieldName)
		{
			if (item == null)
				return null;


			if (item.Fields.All (f => f.Name != fieldName))
				return null;

			string fieldValue = item [fieldName].RawValue;

			if (string.IsNullOrWhiteSpace (fieldValue))
				return null;

			return XElement.Parse (fieldValue);
		}
	}
}


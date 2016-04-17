

namespace HabitatApp.Triggers
{
	using System;
	using Xamarin.Forms;

	public class FadeTriggerAction : TriggerAction<VisualElement>
	{
		public FadeTriggerAction() {
			
		}

		public int StartsFrom { set; get; }

		protected override void Invoke (VisualElement sender)
		{
			sender.Animate("", new Animation( (d)=>{
				var val = StartsFrom==1 ? d : 1-d;
				sender.BackgroundColor = Color.FromRgb(1, val, 1);

			}),
				length:1000, // milliseconds
				easing: Easing.Linear);
		}
	}
}


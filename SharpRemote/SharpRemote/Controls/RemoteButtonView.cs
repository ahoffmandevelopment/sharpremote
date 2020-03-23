using SharpRemote.Behaviors;
using Xamarin.Forms;

namespace SharpRemote.Controls
{
	public class RemoteButtonView : Button
    {
		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			if (BindingContext != null)
			{
				Behaviors.Add(new LongPressBehavior
				{
					BindingContext = BindingContext,
					Command = Command,
					CommandParameter = CommandParameter
				});
			}
		}
	}
}
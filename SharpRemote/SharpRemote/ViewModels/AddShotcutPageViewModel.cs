using Acr.UserDialogs;
using SharpRemote.Models;
using SharpRemote.Sqlite.Models;
using SharpRemote.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SharpRemote.ViewModels
{
	public class AddShotcutPageViewModel : BaseViewModel
    {
		private string buttonText;
		public string ButtonText
		{
			get => buttonText;
			set => UpdateProp(ref buttonText, value);
		}

		private string commandText;
		public string CommandText
		{
			get => commandText;
			set => UpdateProp(ref commandText, value);
		}

		private string category;
		public string Category
		{
			get => category;
			set => UpdateProp(ref category, value);
		}

		private Color backgroundColor = Color.Black;
		public Color BackgroundColor
		{
			get => backgroundColor;
			set => UpdateProp(ref backgroundColor, value);
		}

		private Color fontColor = Color.White;
		public Color FontColor
		{
			get => fontColor;
			set => UpdateProp(ref fontColor, value);
		}

		private RemoteButton remoteButton;

		public void InitViewModel(RemoteButton remoteButton)
		{
			if (remoteButton is null)
			{
				this.remoteButton = new RemoteButton();
			}
			else
			{
				this.remoteButton = remoteButton;

				ButtonText = remoteButton.ButtonText;
				Category = remoteButton.Category;
				CommandText = remoteButton.CommandText;
				BackgroundColor = string.IsNullOrEmpty(remoteButton.BackgroundColorHex)
					? Color.Black
					: Color.FromHex(remoteButton.BackgroundColorHex);
				FontColor = string.IsNullOrEmpty(remoteButton.FontColorHex)
					? Color.White
					: Color.FromHex(remoteButton.FontColorHex);
			}

			WireupCommands();

			MessagingCenter.Unsubscribe<ColorPickerPageViewModel, NamedColor>(this, "ColorPicked");
		}

		public ICommand CancelCommand { get; private set; }

		public ICommand SaveCommand { get; private set; }

		public ICommand SetBackgroundColorCommand { get; private set; }

		public ICommand SetFontColorCommand { get; private set; }

		private void WireupCommands()
		{
			CancelCommand = new Command(async () => await OnCancelAsync());
			SaveCommand = new Command(async () => await OnSaveAsync());
			SetBackgroundColorCommand = new Command(async () => await OnSetBackgroundColorAsync());
			SetFontColorCommand = new Command(async () => await OnSetFontColorAsync());
		}

		private async Task OnSetBackgroundColorAsync()
		{
			MessagingCenter.Unsubscribe<ColorPickerPageViewModel, Color>(this, "ColorPicked");
			var colorPickerPage = new ColorPickerPage();

			MessagingCenter.Subscribe<ColorPickerPageViewModel, Color>(this, "ColorPicked", async (sender, color) =>
			{
				BackgroundColor = color;
				await popupNavigation.PopAsync();
				MessagingCenter.Unsubscribe<ColorPickerPageViewModel, Color>(this, "ColorPicked");
			});

			await popupNavigation.PushAsync(colorPickerPage);
		}

		private async Task OnSetFontColorAsync()
		{
			MessagingCenter.Unsubscribe<ColorPickerPageViewModel, Color>(this, "ColorPicked");
			var colorPickerPage = new ColorPickerPage();
			MessagingCenter.Subscribe<ColorPickerPageViewModel, Color>(this, "ColorPicked", async (sender, color) =>
			{
				FontColor = color;
				await popupNavigation.PopAsync();
				MessagingCenter.Unsubscribe<ColorPickerPageViewModel, Color>(this, "ColorPicked");

			});

			await popupNavigation.PushAsync(colorPickerPage);
		}

		private async Task OnCancelAsync()
		{
			var response = await UserDialogs.Instance.ConfirmAsync("The new button will not be saved. Do you wish to continue?", null, "OK", "Cancel");

			if (response)
			{
				await popupNavigation.PopAsync();
			}
		}

		private async Task OnSaveAsync()
		{
			remoteButton.BackgroundColorHex = BackgroundColor.ToHex();
			remoteButton.ButtonText = ButtonText;
			remoteButton.Category = Category;
			remoteButton.CommandText = CommandText;
			remoteButton.FontColorHex = FontColor.ToHex();

			await App.Database.SaveItemAsync(remoteButton);

			await UserDialogs.Instance.AlertAsync("Your button has been added!", null, "OK");

			await popupNavigation.PopAsync();
		}
	}
}

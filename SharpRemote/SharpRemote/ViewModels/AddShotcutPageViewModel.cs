using Acr.UserDialogs;
using SharpRemote.Sqlite.Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using SharpRemote.Extensions;

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
				BackgroundColor = Color.Black;
				FontColor = Color.White;

				//BackgroundColor = Color.FromHex(remoteButton.BackgroundColorHex);
				//FontColor = Color.FromHex(remoteButton.FontColorHex);
			}

			WireupCommands();

			
		}

		public ICommand CancelCommand { get; private set; }

		public ICommand SaveCommand { get; private set; }

		private void WireupCommands()
		{
			CancelCommand = new Command(async () => await OnCancelAsync());
			SaveCommand = new Command(async () => await OnSaveAsync());
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

using SharpRemote.Extensions;
using SharpRemote.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SharpRemote.ViewModels
{
    public class RemotePageViewModel : BaseViewModel
    {
        public RemotePageViewModel()
        {
            WireupCommands();
        }

        public ICommand KeypadPressedCommand { get; private set; }

        public ICommand PowerOnPressedCommand { get; private set; }

        public ICommand RefreshConnectionPressedCommand { get; private set; }        
        
        private async Task OnKeypadPressedAsync()
        {
            vibrateService.Vibrate(50);
            await popupNavigation.PushAsync(new KeypadPage(), false);
        }

        private async Task OnPowerOnPressedAsync()
        {
            vibrateService.Vibrate(50);

            // Sends command to open up the port, slight delay, then sends the power on command.
            await tcpService.WriteAsync("RSPW2".ToSharpCommandString());
            await Task.Delay(300);
            await tcpService.WriteAsync("POWR1".ToSharpCommandString());
        }

        private async Task OnRefreshConnectionPressedAsync()
        {
            vibrateService.Vibrate(50);
            await tcpService.RefreshConnectionAsync();
        }

        private void WireupCommands()
        {            
            KeypadPressedCommand = new Command(async () => await OnKeypadPressedAsync());
            PowerOnPressedCommand = new Command(async () => await OnPowerOnPressedAsync());
            RefreshConnectionPressedCommand = new Command(async () => await OnRefreshConnectionPressedAsync());
        }
    }
}

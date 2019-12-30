using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using SharpRemote.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SharpRemote.ViewModels
{
    public class RemotePageViewModel : BaseViewModel
    {
        private readonly IPopupNavigation popupNavigation;

        public RemotePageViewModel()
        {
            popupNavigation = PopupNavigation.Instance;

            WireupCommands();
        }

        public ICommand KeypadPressedCommand { get; private set; }

        public ICommand RefreshConnectionPressedCommand { get; private set; }        
        
        private async Task OnKeypadPressedAsync()
        {
            vibrateService.Vibrate(50);
            await popupNavigation.PushAsync(new KeypadPage(), false);
        }

        private async Task OnRefreshConnectionPressedAsync()
        {
            vibrateService.Vibrate();
            await tcpService.RefreshConnectionAsync();
        }

        private void WireupCommands()
        {            
            KeypadPressedCommand = new Command(async () => await OnKeypadPressedAsync());
            RefreshConnectionPressedCommand = new Command(async () => await OnRefreshConnectionPressedAsync());
        }
    }
}

using SharpRemote.Services.Interfaces;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using SharpRemote.Extensions;
using SharpRemote.Views;

namespace SharpRemote.ViewModels
{
    public class RemotePageViewModel
    {
        private readonly ITcpService tcpService;
        private readonly IVibrateService vibrateService;

        public RemotePageViewModel()
        {
            tcpService = DependencyService.Resolve<ITcpService>();
            vibrateService = DependencyService.Resolve<IVibrateService>();

            WireupCommands();
        }

        public ICommand ButtonPressedCommand { get; private set; }

        public ICommand KeypadPressedCommand { get; private set; }

        public ICommand RefreshConnectionPressedCommand { get; private set; } 

        private async Task OnButtonPressedAsync(string parameter)
        {                  
            try
            {
                vibrateService.Vibrate(50);

                await tcpService.WriteAsync($"RCKY{parameter}".ToSharpCommandString());
            }
            catch (Exception)
            {

            }            
        }
        
        private async Task OnKeypadPressedAsync()
        {
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new KeypadPage(), false);
        }

        private async Task OnRefreshConnectionPressedAsync()
        {
            await tcpService.RefreshConnectionAsync();
        }

        private void WireupCommands()
        {
            ButtonPressedCommand = new Command<string>(async (parameter) => await OnButtonPressedAsync(parameter));
            KeypadPressedCommand = new Command(async () => await OnKeypadPressedAsync());
            RefreshConnectionPressedCommand = new Command(async () => await OnRefreshConnectionPressedAsync());
        }
    }
}

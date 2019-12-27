using SharpRemote.Services.Interfaces;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SharpRemote.ViewModels
{
    public class MainPageViewModel
    {
        private readonly ITcpService tcpService;
        private readonly IVibrateService vibrateService;

        public MainPageViewModel()
        {
            tcpService = DependencyService.Resolve<ITcpService>();
            vibrateService = DependencyService.Resolve<IVibrateService>();

            ButtonPressedCommand = new Command<string>(async (parameter) => await OnButtonPressed(parameter));
        }

        public ICommand ButtonPressedCommand { get; private set; }

        private async Task OnButtonPressed(string parameter)
        {                  
            try
            {
                vibrateService.Vibrate(50);

                var command = $"RCKY{parameter}  \r";

                await tcpService.WriteAsync(command);
            }
            catch (Exception)
            {

            }            
        }        
    }
}

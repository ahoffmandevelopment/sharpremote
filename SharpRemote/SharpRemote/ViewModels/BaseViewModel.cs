using SharpRemote.Extensions;
using SharpRemote.Services.Interfaces;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SharpRemote.ViewModels
{
    public class BaseViewModel
    {
        protected readonly ITcpService tcpService;
        protected readonly IVibrateService vibrateService;

        public BaseViewModel()
        {
            tcpService = DependencyService.Resolve<ITcpService>();
            vibrateService = DependencyService.Resolve<IVibrateService>();

            ButtonPressedCommand = new Command<string>(async (parameter) => await OnButtonPressedAsync(parameter));
        }

        public ICommand ButtonPressedCommand { get; private set; }

        public virtual async Task OnButtonPressedAsync(string parameter)
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
    }
}

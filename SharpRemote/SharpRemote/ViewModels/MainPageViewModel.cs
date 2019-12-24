using SharpRemote.Services.Interfaces;
using Sockets.Plugin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SharpRemote.ViewModels
{
    public class MainPageViewModel
    {
        private readonly IVibrateService vibrateService;

        public MainPageViewModel()
        {
            vibrateService = DependencyService.Resolve<IVibrateService>();
            ButtonPressedCommand = new Command<string>(async (parameter) => await OnButtonPressed(parameter));
        }

        public ICommand ButtonPressedCommand { get; private set; }

        private async Task OnButtonPressed(string parameter)
        {
            vibrateService.Vibrate(50);
            var address = "192.168.1.123";
            var port = 10002;
            var command = $"RCKY{parameter}  \r";


            var client = new TcpSocketClient();
            await client.ConnectAsync(address, port);

            // we're connected!

            var bytes = Encoding.ASCII.GetBytes(command);

            // write to the 'WriteStream' property of the socket client to send data
            await client.WriteStream.WriteAsync(bytes, 0, bytes.Length);
            await client.WriteStream.FlushAsync();


            var thing = client.GetConnectedInterfaceAsync();
            await client.DisconnectAsync();
        }
    }
}

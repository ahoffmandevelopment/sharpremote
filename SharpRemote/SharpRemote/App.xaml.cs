using SharpRemote.Services.Interfaces;
using Xamarin.Forms;

namespace SharpRemote
{
    public partial class App : Application
    {
        private readonly ITcpService tcpService;

        public App()
        {
            InitializeComponent();

            tcpService = DependencyService.Resolve<ITcpService>();

            MainPage = new AppShell();            
        }

        protected override async void OnStart()
        {
            await tcpService.ConnectToClientAsync();
        }

        protected override async void OnSleep()
        {
            await tcpService.DisconnectFromClientAsync();
        }

        protected override async void OnResume()
        {
            await tcpService.ConnectToClientAsync();
        }
    }
}

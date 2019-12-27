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

            MainPage = new MainPage();

            tcpService = DependencyService.Resolve<ITcpService>();
        }

        protected override async void OnStart()
        {
            await tcpService.ConnectToClientAsync();
        }

        protected override async void OnSleep()
        {
            await tcpService.DisconnecFromClientAsync();
        }

        protected override async void OnResume()
        {
            await tcpService.ConnectToClientAsync();
        }
    }
}

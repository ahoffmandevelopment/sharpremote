using Xamarin.Forms;

namespace SharpRemote
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();            
        }
    }
}

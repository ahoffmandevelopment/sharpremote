using SharpRemote.Services.Interfaces;
using SharpRemote.Sqlite;
using Xamarin.Forms;

namespace SharpRemote
{
    public partial class App : Application
    {
        static RemoteDatabase database;

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        public static RemoteDatabase Database
        {
            get
            {
                if (database is null)
                {
                    database = new RemoteDatabase(DependencyService.Resolve<IFileSystemService>().AppDataDirectory);
                }

                return database;
            }
        }
    }
}

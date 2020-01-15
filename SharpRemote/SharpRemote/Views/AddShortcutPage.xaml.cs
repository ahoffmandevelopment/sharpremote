using Rg.Plugins.Popup.Pages;
using SharpRemote.Sqlite.Models;
using SharpRemote.ViewModels;
using Xamarin.Forms.Xaml;

namespace SharpRemote.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddShortcutPage : PopupPage
    {
        readonly RemoteButton remoteButton;

        public AddShortcutPage(RemoteButton remoteButton)
        {
            this.remoteButton = remoteButton;

            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext is AddShotcutPageViewModel viewModel)
            {
                viewModel.InitViewModel(remoteButton);
            }
        }
    }
}
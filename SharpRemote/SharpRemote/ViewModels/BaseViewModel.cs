using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using SharpRemote.Extensions;
using SharpRemote.Services.Interfaces;
using SharpRemote.Sqlite.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using SharpRemote.Extensions;

namespace SharpRemote.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected readonly ITcpService tcpService;
        protected readonly IVibrateService vibrateService;

        protected readonly IPopupNavigation popupNavigation;

        public BaseViewModel()
        {
            popupNavigation = PopupNavigation.Instance;

            tcpService = DependencyService.Resolve<ITcpService>();
            vibrateService = DependencyService.Resolve<IVibrateService>();

            ButtonPressedCommand = new Command<string>(async (parameter) => await OnButtonPressedAsync(parameter));
        }

        public ICommand ButtonPressedCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual async Task OnButtonPressedAsync(string parameter)
        {
            try
            {
                vibrateService.Vibrate(50);

                var thing = await App.Database.GetItemsAsync<RemoteButton>();

                await tcpService.WriteAsync(parameter.ToSharpCommandString());
            }
            catch (Exception)
            {

            }
        }

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void UpdateProp<T>(ref T origVar, T newValue, [CallerMemberName]string propertyName = null)
        {
            if (Equals(origVar, newValue))
            {
                return;
            }

            origVar = newValue;

            OnPropertyChanged(propertyName);
        }
    }
}

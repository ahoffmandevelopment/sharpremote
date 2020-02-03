using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace SharpRemote.ViewModels
{
    public class ColorPickerPageViewModel : BaseViewModel
    {
        public ObservableCollection<Color> Colors { get; private set; } = new ObservableCollection<Color>();

        public ColorPickerPageViewModel()
        {
            SelectionChangedCommand = new Command<Color>(SetNamedColor);
        }

        private void SetNamedColor(Color color)
        {
            MessagingCenter.Send(this, "ColorPicked", color);
        }

        public ICommand SelectionChangedCommand { get; private set; }
    }
}

using SharpRemote.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SharpRemote.ViewModels
{
    public class ShortcutPageViewModel : BaseViewModel
    {
        public ObservableCollection<RemoteButtonGroup> Shortcuts { get; private set; } = new ObservableCollection<RemoteButtonGroup>();

        public ShortcutPageViewModel()
        {
            var groupedButtons = new List<RemoteButtonGroup>();

            groupedButtons.Add(new RemoteButtonGroup("Inputs", new List<RemoteButton>
            {
                new RemoteButton
                {
                    ButtonText = "HDMI 1",
                    CommandText = "IAVD1"
                },

                new RemoteButton
                {
                    ButtonText = "HDMI 2",
                    CommandText = "IAVD2"
                },

                new RemoteButton
                {
                    ButtonText = "HDMI 3",
                    CommandText = "IAVD3"
                },

                new RemoteButton
                {
                    ButtonText = "TV",
                    CommandText = "ITVD0"
                }
            }));

            groupedButtons.Add(new RemoteButtonGroup("Channels", new List<RemoteButton>
            {
                new RemoteButton
                {
                    ButtonText = "16.2",
                    CommandText = "DA2P1602"
                },

                new RemoteButton
                {
                    ButtonText = "7.1",
                    CommandText = "DA2P0701"
                },

                new RemoteButton
                {
                    ButtonText = "12.1",
                    CommandText = "DA2P1201"
                }
            }));

            groupedButtons.Add(new RemoteButtonGroup("Volumes", new List<RemoteButton>
            {
                new RemoteButton
                {
                    ButtonText = "10",
                    CommandText = "VOLM010"
                },

                new RemoteButton
                {
                    ButtonText = "20",
                    CommandText = "VOLM020"
                },

                new RemoteButton
                {
                    ButtonText = "30",
                    CommandText = "VOLM030"
                }
            }));

            Shortcuts.Clear();

            groupedButtons
                .OrderBy(gb => gb.Name)
                .ToList()
                .ForEach(gb => Shortcuts.Add(gb));

            groupedButtons = null;
        }
    }
}

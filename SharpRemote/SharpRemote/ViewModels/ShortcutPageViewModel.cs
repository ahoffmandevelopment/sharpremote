using Rg.Plugins.Popup.Events;
using SharpRemote.Models;
using SharpRemote.Sqlite.Models;
using SharpRemote.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SharpRemote.ViewModels
{
    public class ShortcutPageViewModel : BaseViewModel
    {
        public ObservableCollection<RemoteButtonGroup> Shortcuts { get; private set; } = new ObservableCollection<RemoteButtonGroup>();

        public ICommand AddShortcutPressedCommand { get; private set; }

        private List<RemoteButtonGroup> groupedButtons;

        public ShortcutPageViewModel()
        {
            AddShortcutPressedCommand = new Command(async () => await OnAddShortcutPressedAsync());

            Task.Run(async () => await ResetShortcuts());
        }

        private async Task OnAddShortcutPressedAsync()
        {
            var page = new AddShortcutPage(null);

            await popupNavigation.PushAsync(page);

            popupNavigation.Popping += PopupNavigation_Popping;
        }

        private async void PopupNavigation_Popping(object sender, PopupNavigationEventArgs e)
        {
            popupNavigation.Popping -= PopupNavigation_Popping;
            await ResetShortcuts();
        }

        private async Task ResetShortcuts()
        {
            Shortcuts.Clear();

            groupedButtons = new List<RemoteButtonGroup>();

            GetDefaultButtons();

            await GetCustomButtons();

            groupedButtons
                .OrderBy(gb => gb.Name)
                .ToList()
                .ForEach(gb => Shortcuts.Add(gb));

            groupedButtons = null;
        }

        private void GetDefaultButtons()
        {
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
        }

        private async Task GetCustomButtons()
        {
            var buttons = await App.Database.GetItemsAsync<RemoteButton>();

            if (buttons.Any())
            {
                buttons.ForEach(b =>
                {
                    if (groupedButtons.Any(s => s.Name == b.Category))
                    {
                        var group = groupedButtons.First(s => s.Name == b.Category).ToList();
                        group.Add(b);
                        groupedButtons.First(s => s.Name == b.Category).Clear();
                        groupedButtons.First(s => s.Name == b.Category).AddRange(group);
                    }
                    else
                    {
                        groupedButtons.Add(new RemoteButtonGroup(b.Category, new List<RemoteButton>
                        {
                            new RemoteButton
                            {
                                ButtonText = b.ButtonText,
                                BackgroundColorHex = b.BackgroundColorHex,
                                Category = b.Category,
                                CommandText = b.CommandText,
                                FontColorHex = b.FontColorHex,
                                ID = b.ID
                            }
                        }));
                    }
                });
            }
        }
    }
}

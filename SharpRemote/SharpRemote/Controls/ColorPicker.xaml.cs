using SharpRemote.Extensions;
using SharpRemote.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SharpRemote.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ColorPicker : ContentView
    {
        public static readonly BindableProperty ItemsProperty =
            BindableProperty.Create(nameof(Items), typeof(IEnumerable<Color>), typeof(ColorPicker), null, propertyChanged: OnItemsPropertyChanged);

        public static readonly BindableProperty SelectionChangedCommandProperty =
            BindableProperty.Create(nameof(SelectionChangedCommand), typeof(ICommand), typeof(ColorPicker), default(ICommand));

        public ICommand SelectionChangedCommand
        {
            get => (ICommand)GetValue(SelectionChangedCommandProperty);
            set => SetValue(SelectionChangedCommandProperty, value);
        }

        private static void OnItemsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ColorPicker colorPicker)
            {
                colorPicker.NamedColors.Clear();

                if (newValue is IEnumerable<Color> colors && colors.Any())
                {
                    colors.Select(c => c.ToNamedColor())
                        .OrderBy(nc => nc.FriendlyName)
                        .ToList()
                        .ForEach(nc => colorPicker.NamedColors.Add(nc));
                }
                else
                {
                    colorPicker.GetAllNamedColors()
                       .OrderBy(nc => nc.FriendlyName)
                       .ToList()
                       .ForEach(nc => colorPicker.NamedColors.Add(nc));
                }                
            }
        }

        public IEnumerable<Color> Items
        {
            get => (IEnumerable<Color>)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        public ObservableCollection<NamedColor> NamedColors { get; private set; } = new ObservableCollection<NamedColor>();

        public ColorPicker()
        {
            InitializeComponent();
        }

        private void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is NamedColor namedColor)
            {
                SelectionChangedCommand?.Execute(namedColor.Color);
            }
        }

        public List<NamedColor> GetAllNamedColors()
        {
            var namedColors = new List<NamedColor>();

            foreach (var fieldInfo in typeof(Color).GetRuntimeFields())
            {
                if (fieldInfo.IsPublic &&
                    fieldInfo.IsStatic &&
                    fieldInfo.FieldType == typeof(Color))
                {
                    var color = (Color)fieldInfo.GetValue(null);

                    namedColors.Add(new NamedColor
                    {
                        Name = fieldInfo.Name,
                        FriendlyName = fieldInfo.Name.ToFriendlyName(),
                        Color = color,
                        HexDisplay = color.ToHex()
                    });
                }
            }

            return namedColors;
        }

    }
}
using SharpRemote.Models;
using System.Collections.Generic;
using System.Reflection;
using Xamarin.Forms;

namespace SharpRemote.Extensions
{
    public static class ColorExtensions
    {
        public static NamedColor ToNamedColor(this Color input)
        {
            NamedColor namedColor = null;

            foreach (var fieldInfo in typeof(Color).GetRuntimeFields())
            {
                if (fieldInfo.IsPublic &&
                    fieldInfo.IsStatic &&
                    fieldInfo.FieldType == typeof(Color))
                {
                    var color = (Color)fieldInfo.GetValue(null);

                    if (color.Equals(input))
                    {
                        namedColor = new NamedColor
                        {
                            Name = fieldInfo.Name,
                            FriendlyName = fieldInfo.Name.ToFriendlyName(),
                            Color = color,
                            HexDisplay = color.ToHex()
                        };

                        break;
                    }
                }
            }

            return namedColor;
        }
    }
}

using System;
using System.Text;

namespace SharpRemote.Extensions
{
    public static class StringExtensions
    {
        public static string ToFriendlyName(this string input)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Clear();
            int index = 0;

            foreach (var ch in input)
            {
                if (index != 0 && char.IsUpper(ch))
                {
                    stringBuilder.Append(' ');
                }
                stringBuilder.Append(ch);
                index++;
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Takes a string, right pads it to a length of 8, and adds a carriage return.
        /// </summary>
        /// <param name="input">The string to format.</param>
        /// <returns>The formatted command string.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if input string is null.</exception>
        public static string ToSharpCommandString(this string input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input), "Input string cannot be null.");
            }

            return $"{input.PadRight(8)}\r";
        }
    }
}

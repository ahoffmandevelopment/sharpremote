namespace SharpRemote.Extensions
{
    public static class StringExtensions
    {
        public static string ToSharpCommandString(this string input)
        {
            return $"{input.PadRight(8)}\r";
        }
    }
}

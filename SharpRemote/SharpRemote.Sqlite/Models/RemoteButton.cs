using System;
using System.Collections.Generic;

namespace SharpRemote.Sqlite.Models
{
    public class RemoteButton : BaseSqliteItem
    {       
        public string ButtonText { get; set; }

        public string CommandText { get; set; }

        public string BackgroundColorHex { get; set; }

        public string FontColorHex { get; set; }

        public string Category { get; set; }

        public override bool Equals(object obj)
        {
            return obj is RemoteButton button &&
                   ButtonText == button.ButtonText &&
                   CommandText == button.CommandText &&
                   BackgroundColorHex == button.BackgroundColorHex &&
                   FontColorHex == button.FontColorHex &&
                   Category == button.Category;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ButtonText, CommandText, BackgroundColorHex, FontColorHex, Category);
        }

        public static bool operator ==(RemoteButton left, RemoteButton right)
        {
            return EqualityComparer<RemoteButton>.Default.Equals(left, right);
        }

        public static bool operator !=(RemoteButton left, RemoteButton right)
        {
            return !(left == right);
        }
    }
}

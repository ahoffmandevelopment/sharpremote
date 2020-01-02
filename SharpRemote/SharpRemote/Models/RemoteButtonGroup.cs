using System.Collections.Generic;

namespace SharpRemote.Models
{
    public class RemoteButtonGroup : List<RemoteButton>
    {
        public RemoteButtonGroup(string name, List<RemoteButton> buttons) : base(buttons)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}

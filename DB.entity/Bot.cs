using System;
using System.Collections.Generic;

#nullable disable

namespace Messenger.DB.entity
{
    public partial class Bot
    {
        public Bot()
        {
            Commands = new HashSet<Command>();
        }

        public int Id { get; set; }
        public string DescriptionBot { get; set; }

        public virtual User IdNavigation { get; set; }
        public virtual ICollection<Command> Commands { get; set; }
    }
}

using System.Collections.Generic;

#nullable disable

namespace Data.Entity
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

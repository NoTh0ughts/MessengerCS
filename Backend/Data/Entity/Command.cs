#nullable disable

namespace Data.Entity
{
    public partial class Command
    {
        public int Id { get; set; }
        public string NameCommand { get; set; }
        public int IdBot { get; set; }

        public virtual Bot IdBotNavigation { get; set; }
    }
}

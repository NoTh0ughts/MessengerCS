#nullable disable

namespace Data.Entity
{
    public partial class UserDialog
    {
        public int UserId { get; set; }
        public int DialogId { get; set; }

        public virtual Dialog Dialog { get; set; }
        public virtual User User { get; set; }
    }
}

#nullable disable

namespace Data.Entity
{
    public partial class Content
    {
        public int Id { get; set; }
        public string RefToContent { get; set; }
        public int MessageId { get; set; }

        public virtual Message Message { get; set; }
    }
}

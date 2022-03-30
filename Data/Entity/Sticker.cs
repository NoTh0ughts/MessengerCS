using System.Collections.Generic;

#nullable disable

namespace Data.Entity
{
    public partial class Sticker
    {
        public Sticker()
        {
            MessageStickers = new HashSet<MessageSticker>();
        }

        public int Id { get; set; }
        public string RefPack { get; set; }
        public string TextSticker { get; set; }
        public string RefImage { get; set; }

        public virtual ICollection<MessageSticker> MessageStickers { get; set; }
    }
}

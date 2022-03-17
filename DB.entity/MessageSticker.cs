using System;
using System.Collections.Generic;

#nullable disable

namespace Messenger.DB.entity
{
    public partial class MessageSticker
    {
        public int MessageId { get; set; }
        public int StickerId { get; set; }

        public virtual Message Message { get; set; }
        public virtual Sticker Sticker { get; set; }
    }
}

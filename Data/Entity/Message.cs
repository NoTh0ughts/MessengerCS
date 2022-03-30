using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Entity
{
    public partial class Message
    {
        public Message()
        {
            Contents = new HashSet<Content>();
        }

        public int Id { get; set; }
        public DateTime SendDate { get; set; }
        public string TextMessage { get; set; }
        public int UserAccessId { get; set; }

        public virtual UserAccess UserAccess { get; set; }
        public virtual MessageSticker MessageSticker { get; set; }
        public virtual ICollection<Content> Contents { get; set; }
    }
}

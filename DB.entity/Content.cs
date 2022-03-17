using System;
using System.Collections.Generic;

#nullable disable

namespace Messenger.DB.entity
{
    public partial class Content
    {
        public int Id { get; set; }
        public string RefToContent { get; set; }
        public int MessageId { get; set; }

        public virtual Message Message { get; set; }
    }
}
